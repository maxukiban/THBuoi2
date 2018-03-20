namespace Bai1_Client
{
    partial class frClient
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
            this.web = new System.Windows.Forms.WebBrowser();
            this.btnGui = new System.Windows.Forms.Button();
            this.rtbGhiChat = new System.Windows.Forms.RichTextBox();
            this.btnNhac = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbNhapIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.Location = new System.Drawing.Point(42, 41);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(448, 147);
            this.web.TabIndex = 22;
            this.web.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            // 
            // btnGui
            // 
            this.btnGui.Location = new System.Drawing.Point(404, 249);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(86, 65);
            this.btnGui.TabIndex = 21;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtbGhiChat
            // 
            this.rtbGhiChat.Location = new System.Drawing.Point(42, 239);
            this.rtbGhiChat.Name = "rtbGhiChat";
            this.rtbGhiChat.Size = new System.Drawing.Size(347, 75);
            this.rtbGhiChat.TabIndex = 20;
            this.rtbGhiChat.Text = "";
            this.rtbGhiChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbGhiChat_KeyDown);
            this.rtbGhiChat.Leave += new System.EventHandler(this.rtbGhiChat_Leave);
            // 
            // btnNhac
            // 
            this.btnNhac.Location = new System.Drawing.Point(444, 194);
            this.btnNhac.Name = "btnNhac";
            this.btnNhac.Size = new System.Drawing.Size(46, 30);
            this.btnNhac.TabIndex = 19;
            this.btnNhac.Text = "Nhạc";
            this.btnNhac.UseVisualStyleBackColor = true;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(364, 194);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(74, 30);
            this.btnFile.TabIndex = 18;
            this.btnFile.Text = "File / Hình";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "IP của server";
            // 
            // txbNhapIP
            // 
            this.txbNhapIP.Location = new System.Drawing.Point(115, 12);
            this.txbNhapIP.Name = "txbNhapIP";
            this.txbNhapIP.ReadOnly = true;
            this.txbNhapIP.Size = new System.Drawing.Size(99, 20);
            this.txbNhapIP.TabIndex = 17;
            this.txbNhapIP.Visible = false;
            // 
            // frClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 325);
            this.Controls.Add(this.txbNhapIP);
            this.Controls.Add(this.web);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.rtbGhiChat);
            this.Controls.Add(this.btnNhac);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.label1);
            this.Name = "frClient";
            this.Text = "Chat - Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frClient_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser web;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.RichTextBox rtbGhiChat;
        private System.Windows.Forms.Button btnNhac;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbNhapIP;
    }
}

