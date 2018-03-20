namespace Bai1
{
    partial class frServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnGui = new System.Windows.Forms.Button();
            this.rtbGhiChat = new System.Windows.Forms.RichTextBox();
            this.btnNhac = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.web = new System.Windows.Forms.WebBrowser();
            this.panelKoCoClient = new System.Windows.Forms.Panel();
            this.txbNhapIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelKoCoClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGui
            // 
            this.btnGui.Location = new System.Drawing.Point(397, 249);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(86, 65);
            this.btnGui.TabIndex = 14;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // rtbGhiChat
            // 
            this.rtbGhiChat.Location = new System.Drawing.Point(35, 239);
            this.rtbGhiChat.Name = "rtbGhiChat";
            this.rtbGhiChat.Size = new System.Drawing.Size(347, 75);
            this.rtbGhiChat.TabIndex = 13;
            this.rtbGhiChat.Text = "";
            this.rtbGhiChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbGhiChat_KeyDown);
            this.rtbGhiChat.Leave += new System.EventHandler(this.rtbGhiChat_Leave);
            // 
            // btnNhac
            // 
            this.btnNhac.Location = new System.Drawing.Point(437, 194);
            this.btnNhac.Name = "btnNhac";
            this.btnNhac.Size = new System.Drawing.Size(46, 30);
            this.btnNhac.TabIndex = 12;
            this.btnNhac.Text = "Nhạc";
            this.btnNhac.UseVisualStyleBackColor = true;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(357, 194);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(74, 30);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "File / Hình";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "IP của client kết nối đến";
            // 
            // web
            // 
            this.web.Location = new System.Drawing.Point(35, 41);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(448, 147);
            this.web.TabIndex = 15;
            this.web.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.web.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webMain_Navigating);
            // 
            // panelKoCoClient
            // 
            this.panelKoCoClient.Controls.Add(this.txbNhapIP);
            this.panelKoCoClient.Controls.Add(this.label2);
            this.panelKoCoClient.Location = new System.Drawing.Point(161, 12);
            this.panelKoCoClient.Name = "panelKoCoClient";
            this.panelKoCoClient.Size = new System.Drawing.Size(192, 20);
            this.panelKoCoClient.TabIndex = 16;
            // 
            // txbNhapIP
            // 
            this.txbNhapIP.Location = new System.Drawing.Point(0, 0);
            this.txbNhapIP.Name = "txbNhapIP";
            this.txbNhapIP.ReadOnly = true;
            this.txbNhapIP.Size = new System.Drawing.Size(192, 20);
            this.txbNhapIP.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(35, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chưa có client kết nối";
            // 
            // frServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 333);
            this.Controls.Add(this.panelKoCoClient);
            this.Controls.Add(this.web);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.rtbGhiChat);
            this.Controls.Add(this.btnNhac);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.label1);
            this.Name = "frServer";
            this.Text = "frServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frServer_FormClosing);
            this.panelKoCoClient.ResumeLayout(false);
            this.panelKoCoClient.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.RichTextBox rtbGhiChat;
        private System.Windows.Forms.Button btnNhac;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser web;
        private System.Windows.Forms.Panel panelKoCoClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbNhapIP;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}