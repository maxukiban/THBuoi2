using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Bai1.Class
{
    class NguoiGui : System.IEquatable<NguoiGui>
    {
        private Button _button;
        private string _nick = "";
        public string Address { get; set; }
        public int Port { get; set; }
        public Color Color { get; set; }
        public TcpClient Tcp { get; set; }
        public string NickName
        {
            get
            {
                if (_nick == "")
                    return Address + ":" + Port;
                else
                    return _nick;
            }
            set
            {
                _nick = value;
            }
        }
        public Button But
        {
            get
            {
                if (Address != "Me")
                {
                    _button.Text = NickName;
                    _button.BackColor = Color;
                    return _button;
                }
                else
                    return null;
            }
        }

        public delegate void CallBack();        // Khai báo delegate
        public CallBack call;       // Biến delegate
        public NguoiGui()
        {
            Random ran = new Random();
            Color = Color.FromArgb(ran.Next(100, 240), ran.Next(100, 240), ran.Next(100, 240));

            _button = new Button();
            _button.Dock = DockStyle.Top;
            _button.Name = "btn";
            _button.FlatStyle = FlatStyle.Flat;
            _button.Size = new Size(150, 40);
            _button.FlatAppearance.BorderSize = 0;
            _button.Padding = new Padding(0);
            _button.UseVisualStyleBackColor = false;

        }

        public static NguoiGui Me
        {
            get
            {
                return new NguoiGui() { Address = "Me", Port = CaiDat.Port, NickName = "Me", Tcp = null, Color = Color.FromName("0") };
            }
        }

        public bool Equals(NguoiGui other)
        {
            if (other == null)
                return false;
            return (
                object.ReferenceEquals(this.Address, other.Address) ||
                this.Address != null &&
                this.Address.Equals(other.Address)
            ) &&
            (
                object.ReferenceEquals(this.Port, other.Port) ||
                this.Port != null &&
                this.Port.Equals(other.Port)
            ) &&
            (
                object.ReferenceEquals(this.Color, other.Color) ||
                this.Color != null &&
                this.Color.Equals(other.Color)
            ) &&
            (
                object.ReferenceEquals(this.Tcp, other.Tcp) ||
                this.Tcp != null &&
                this.Tcp.Equals(other.Tcp)
            ) &&
            (
                object.ReferenceEquals(this.NickName, other.NickName) ||
                this.NickName != null &&
                this.NickName.Equals(other.NickName)
            );
        }
    }
}
