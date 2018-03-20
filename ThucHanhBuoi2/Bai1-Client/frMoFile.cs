using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Bai1_Client
{
    public partial class frMoFile : Form
    {
        public frMoFile()
        {
            InitializeComponent();
        }
        string Address = "";       
        public frMoFile(string address)
        {
            InitializeComponent();
            Address = address.Replace("(~*)", ":");
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                // Bóng đổ
                cp.ClassStyle |= CS_DROPSHADOW;
                // Load các control cùng lúc
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(Address))
                try
                {
                    Process.Start(Address);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Không thể mở file");
                }
            else
                MessageBox.Show("Tập tin không tồn tại");
        }

        private void BtnMoThuMuc_Click(object sender, EventArgs e)
        {
            if (File.Exists(Address))
                try
                {
                    Process.Start("explorer.exe", " /select, " + Address);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Không thể mở file");
                }
            else
                MessageBox.Show("Tập tin không tồn tại");
        }
    }
}
