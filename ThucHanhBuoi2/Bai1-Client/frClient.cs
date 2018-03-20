using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Bai1.Class;
using System.Threading;
using System.Net.Sockets;

namespace Bai1_Client
{
    public partial class frClient : Form
    {
        #region Khai Báo biến
        /// <summary>
        /// Di chuyển form
        /// </summary>      

        DoiThoai conversation = new DoiThoai();
        NguoiGui sender;
        Thread t;
        List<string> lstFileName = new List<string>();

        private const int BufferSize = 1024;
        byte[] SendingBuffer;
        #endregion

        #region Hàm tự tạo
        /// <summary>
        /// Gửi tin nhắn thông thường
        /// </summary>
        /// <param name="content"></param>
        void SendMessage(string content, string path)
        {
            if (CaiDat.TcpServer.Connected)
            {
                TinNhan mes = new TinNhan();
                mes.Sender = NguoiGui.Me;
                mes.Content = content;
                mes.Time = DateTime.Now;
                conversation.ThemTinNhan(mes);
                RefreshWeb();
                // Gửi cho server
                try
                {
                    NetworkStream ns = CaiDat.TcpServer.GetStream();
                    StreamWriter sw = new StreamWriter(ns);
                    if (path == "")
                    {
                        sw.WriteLine(content);      // Gửi tin nhắn
                        sw.Flush();
                    }
                    else        // Gửi tập tin
                    {
                        DoSendFile(sw, path);
                    }
                    return;
                }
                catch { }
            }
            // Nếu ko ghi được
            while (!CaiDat.TcpServer.Connected)
            {
                // Thử kết nối lại
                if (MessageBox.Show("Mất kết nối tới server\nThử kết nối lại?", "Lỗi", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    try
                    {
                        CaiDat.TcpServer = new TcpClient(CaiDat.Server, CaiDat.Port);
                    }
                    catch { }
                else
                    break;
            }
        }
        /// <summary>
        /// Thực hiện gửi file tới server
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="path"></param>
        void DoSendFile(StreamWriter sw, string path)
        {
            Cursor = Cursors.WaitCursor;
            sw.WriteLine(CaiDat.MarkSendFile);
            sw.Flush();
            sw.WriteLine(Path.GetFileName(path));
            sw.Flush();

            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            int numPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(file.Length) / Convert.ToDouble(BufferSize)));           
            sw.WriteLine(numPackets);       // Gửi số packet
            sw.Flush();

            long TotalLength = file.Length;
            int CurrentPacketLength;
            // Gửi lần lượt từng packet
            for (int i = 0; i < numPackets; i++)
            {
                if (TotalLength > BufferSize)
                {
                    CurrentPacketLength = BufferSize;
                    TotalLength = TotalLength - CurrentPacketLength;
                }
                else
                    CurrentPacketLength = (int)TotalLength;
                SendingBuffer = new byte[CurrentPacketLength];
                // Đọc từ file
                file.Read(SendingBuffer, 0, CurrentPacketLength);
                sw.WriteLine(System.Convert.ToBase64String(SendingBuffer));
                sw.Flush();
                                
            }
            file.Close();
            Cursor = Cursors.Default;           
        }
        /// <summary>
        /// Gửi hình ảnh & tập tin đính kèm
        /// </summary>
        /// <param name="path"></param>
        void SendImage(string path)
        {
            Uri url = new Uri(path);
            string address = url.AbsoluteUri;
            string extension = Path.GetExtension(path).ToLower();
            string content = "<a href='" + url.PathAndQuery.Replace(":", "(~*)") + "'>" + ((extension == ".jpg" || extension == ".png") ? "<img src='" + address + "' style='max-width:300px'/><br/>" : "") + "<b>" + Path.GetFileName(path) + "</b></a> (" + KichThuocFile.SizeSuffix(new FileInfo(path).Length) + ")";

            SendMessage(content, path);
        }
        /// <summary>
        /// Lắng nghe từ server
        /// </summary>
        void Listening()
        {
            NetworkStream ns = CaiDat.TcpServer.GetStream();
            StreamReader sr = new StreamReader(ns);
            string s;
            while (CaiDat.TcpServer.Connected)
            {
                try
                {
                    s = sr.ReadLine();
                    if (s != null)
                    {
                        if (s == CaiDat.MarkSendFile)       // Nếu gửi file
                        {
                            s = DoReciveFile(sr);
                        }
                        // Add number
                        conversation.ThemTinNhan(new TinNhan() { Content = s, Sender = sender, Time = DateTime.Now });
                        RefreshWeb();
                        continue;
                    }
                }
                catch { }
                // Nếu ko đọc được hoặc nội dung đọc về là null
                while (!CaiDat.TcpServer.Connected)
                {
                    // Thử kết nối lại
                    if (MessageBox.Show("Mất kết nối tới server\nThử kết nối lại?", "Lỗi", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        try
                        {
                            CaiDat.TcpServer = new TcpClient(CaiDat.Server, CaiDat.Port);
                        }
                        catch { }
                    else
                    {
                        Thread.CurrentThread.Abort();       // Ngắt kết nối
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Thực hiện nhận file
        /// </summary>
        /// <param name="sr"></param>
        /// <returns>FileName đã nhận</returns>
        string DoReciveFile(StreamReader sr)
        {
            string filename = sr.ReadLine();        // Tên file
            lstFileName.Add(filename);

            int numPacket = Convert.ToInt32(sr.ReadLine());

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    
                    Cursor = Cursors.WaitCursor;                   
                    FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    for (int i = 0; i < numPacket; i++)
                    {
                        string base64 = sr.ReadLine();      // Nội dung file
                        SendingBuffer = System.Convert.FromBase64String(base64);
                        file.Write(SendingBuffer, 0, SendingBuffer.Length);
                    }
                    file.Close();
                    Cursor = Cursors.Default;                  
                }));
            }

            string path = Directory.GetCurrentDirectory() + "/" + filename;
            Uri url = new Uri(path);
            string address = url.AbsoluteUri;
            string extension = Path.GetExtension(filename).ToLower();
            string s = "<a href='" + url.PathAndQuery.Replace(":", "(~*)") + "'>" + ((extension == ".jpg" || extension == ".png") ? "<img src='" + address + "' style='max-width:300px'/><br/>" : "") + "<b>" + filename + "</b></a> (" + KichThuocFile.SizeSuffix(new FileInfo(filename).Length) + ")";

            return s;
        }
        /// <summary>
        /// Nạp lại nội dung tin nhắn
        /// </summary>
        void RefreshWeb()
        {
            // Chưa scroll xuống cuối được => có thể phải thêm nút js
            web.Invoke(new Action(() =>
            {
                web.Document.Write(conversation.GetHTML);
                web.Refresh();
                //  webMain.Document.Window.ScrollTo(0, webMain.Document.Body.ScrollRectangle.Height);
            }));
        }

        private void webMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = true;
            if (e.Url.ToString() != "about:blank")
            {
                string url = e.Url.PathAndQuery;
                frMoFile frm = new frMoFile(url);
                frm.ShowDialog();
            }
        }    
        #endregion

        #region Forms
        public frClient()
        {
            InitializeComponent();
            this.Padding = new Padding(1);
            txbNhapIP.Visible = true;
            txbNhapIP.Text = CaiDat.Server;
            sender = new NguoiGui() { Address = CaiDat.Server, Port = CaiDat.Port, Tcp = CaiDat.TcpServer };
            t = new Thread(Listening);
            t.Start();
        }

        private void frClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var f in lstFileName)
            {
                if (File.Exists(f))
                    File.Delete(f);
            }
            t.Abort();
            CaiDat.TcpServer.Close();
        }

        #endregion


        #region Controls

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (rtbGhiChat.Text != "")
            {
                SendMessage(rtbGhiChat.Text, "");
                rtbGhiChat.Text = "";
                rtbGhiChat.Focus();
            }
        }

        private void rtbGhiChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnGui.PerformClick();
                e.Handled = true;
            }

        }

        private void rtbGhiChat_Leave(object sender, EventArgs e)
        {
            rtbGhiChat.Focus();
        }       

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.RestoreDirectory = true;
            dia.Title = "Chọn tập tin đính kèm";
            if (dia.ShowDialog() == DialogResult.OK)
                SendImage(dia.FileName);
        }

        #endregion        
       
    }
}
