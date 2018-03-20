using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bai1.Class;
using System.Net.Sockets;

namespace Bai1
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
            CaiDat.Mode = CaiDat.Modes.Server;           
            Application.Run(new frServer());
        }
    }
}
