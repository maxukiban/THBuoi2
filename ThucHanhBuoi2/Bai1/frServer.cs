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
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Bai1
{
    public partial class frServer : Form
    {     

        #region Khai Báo biến
        /// <summary>
        /// Di chuyển form
        /// </summary>    

        DoiThoai conversation = new DoiThoai();
        // Listener
        TcpListener tcpListen;

        List<TcpClient> lstClient = new List<TcpClient>();

        bool _running = false;
        Thread t;

        List<string> lstFileName = new List<string>();
        private const int BufferSize = 1024;
        byte[] SendingBuffer;
        #endregion

        #region Hàm tự tạo
        /// <summary>
        /// Gửi tin nhắn 
        /// </summary>
        /// <param name="content"></param>
        void SendMessage(string content, string path)
        {
            if (lstClient.Count == 0)        // Ko có client nào
            {
                MessageBox.Show("Chờ client kết nối đến...");
                return;
            }
            TinNhan mes = new TinNhan();
            mes.Sender = NguoiGui.Me;
            mes.Content = content;
            mes.Time = DateTime.Now;
            conversation.ThemTinNhan(mes);
            RefreshWeb();
            // Tiến hành gửi qua tcp cho các client
            for (int i = 0; i < lstClient.Count; i++)
            {
                if (lstClient[i].Connected)
                {
                    NetworkStream ns = lstClient[i].GetStream();
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
                }
                else        // Client ko kết nối nữa
                {
                    RemoveClient(lstClient[i]);
                    i--;
                }
            }

        }
        /// <summary>
        /// Thực hiện gửi file tới server
        /// Ở send server thì gửi cho nhiều client nên sẽ lâu hơn
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
        /// Gửi hình ảnh
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
        /// Lắng nghe các client kết nối đến
        /// </summary>
        void Listening()
        {
            while (_running)
            {
                try
                {
                    TcpClient client = tcpListen.AcceptTcpClient();
                    lstClient.Add(client);
                    NguoiGui sender = conversation.ThemClient(client);
                    sender.call = new NguoiGui.CallBack(RefreshWeb);      // Khi sender thực hiện delegate nó sẽ gọi lại Refresh Web
                    // Tạo 1 thread mới để lắng nghe client này
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                    LoadListSender();
                }
                catch { }
            }
        }
        /// <summary>
        /// Lắng nghe từng client 
        /// </summary>
        /// <param name="client"></param>
        void HandleClient(object client)
        {
            TcpClient tcpclient = (TcpClient)client;
            NguoiGui sender = conversation.GetSender(tcpclient);
            TinNhan mes;
            NetworkStream ns = tcpclient.GetStream();
            StreamReader sr = new StreamReader(ns);
            string s;
            while (tcpclient.Connected)
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
                        mes = new TinNhan() { Content = s, Sender = sender, Time = DateTime.Now };
                        conversation.ThemTinNhan(mes);
                        RefreshWeb();
                        continue;
                    }
                }
                catch { }
                // Ko nhận được dữ liệu nữa có nghĩa là nó ngắt kết nối rồi
                RemoveClient(tcpclient);
                Thread.CurrentThread.Abort();
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
            string s = "<a href='" + url.PathAndQuery.Replace(":", "(~*)") + "'>" + ((extension == ".jpg" || extension == ".png") ? "<img src='" + address + "' style='max-width:300px'/><br/>" : "") + "<b>" + Path.GetFileName(path) + "</b></a> (" + KichThuocFile.SizeSuffix(new FileInfo(path).Length) + ")";

            return s;
        }
        /// <summary>
        /// XÓa client
        /// </summary>
        /// <param name="client"></param>
        void RemoveClient(TcpClient client)
        {
            if (lstClient.Contains(client))
                lstClient.Remove(client);
            conversation.RemoveClient(client);
            LoadListSender();
        }
        /// <summary>
        /// Hiển thị danh sách client lên view
        /// </summary>
        void LoadListSender()
        {
            // List button
            if (_running && this.InvokeRequired)
                Invoke(new Action(() =>
                {
                    if (lstClient.Count == 0)
                    {
                        txbNhapIP.Visible = false;
                        return;
                    }
                    txbNhapIP.SuspendLayout();
                    txbNhapIP.Controls.Clear();                 
                    foreach (var sender in conversation.Senders)
                    {
                        if (sender.But != null)
                        {
                            toolTip1.SetToolTip(sender.But, "Địa chỉ " + sender.Address + " cổng " + sender.Port);
                            txbNhapIP.Controls.Add(sender.But);
                        }
                    }

                    txbNhapIP.ResumeLayout();
                    txbNhapIP.Visible = true;
                    
                    // Update the form
                    this.PerformLayout();
                }));
        }
        /// <summary>
        /// Load lại nội dung tin nhắn
        /// </summary>
        void RefreshWeb()
        {
            web.Invoke(new Action(() =>
            {
                web.Document.Write(conversation.GetHTML);
                web.Refresh();
            }));
        }
        #endregion

        #region Forms
        public frServer()
        {
            InitializeComponent();
            this.Padding = new Padding(1);

            tcpListen = new TcpListener(IPAddress.Any, CaiDat.Port);
            try
            {
                tcpListen.Start();
            }
            catch
            {
                MessageBox.Show("Không khởi tạo được kết nối", "Lỗi");
                Application.Exit();
                return;
            }
            _running = true;

            t = new Thread(Listening);
            t.Start();
        }

        private void frServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var f in lstFileName)
            {
                if (File.Exists(f))
                    File.Delete(f);
            }
            _running = false;

            foreach (var client in lstClient)
            {
                client.Close();
            }
            tcpListen.Stop();
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


        #region Controls
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }   
        
        #endregion

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.RestoreDirectory = true;
            dia.Title = "Chọn tập tin đính kèm";
            if (dia.ShowDialog() == DialogResult.OK)
                SendImage(dia.FileName);
        }

        private void btnGui_Click(object sender, EventArgs e)
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
    }
}
