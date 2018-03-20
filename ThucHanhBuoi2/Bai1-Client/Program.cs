using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bai1.Class;
using System.Net.Sockets;

namespace Bai1_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CaiDat.Server = "127.0.0.1";
            CaiDat.Port = 2910;
            CaiDat.MarkSendFile = "@#$fileNVN##$#@";
            CaiDat.Mode = CaiDat.Modes.Client;
            try
            {
                CaiDat.TcpServer = new TcpClient(CaiDat.Server, CaiDat.Port);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tới server");
                return;
            }          
            Application.Run(new frClient());
        }
    }
}
