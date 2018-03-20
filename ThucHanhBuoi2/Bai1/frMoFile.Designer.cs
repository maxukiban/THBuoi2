namespace Bai1
{
    partial class frMoFile
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
            this.btnMoFile = new System.Windows.Forms.Button();
            this.BtnMoThuMuc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMoFile
            // 
            this.btnMoFile.Location = new System.Drawing.Point(25, 21);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(226, 32);
            this.btnMoFile.TabIndex = 0;
            this.btnMoFile.Text = "Mở file";
            this.btnMoFile.UseVisualStyleBackColor = true;
            this.btnMoFile.Click += new System.EventHandler(this.btnMoFile_Click);
            // 
            // BtnMoThuMuc
            // 
            this.BtnMoThuMuc.Location = new System.Drawing.Point(280, 21);
            this.BtnMoThuMuc.Name = "BtnMoThuMuc";
            this.BtnMoThuMuc.Size = new System.Drawing.Size(226, 32);
            this.BtnMoThuMuc.TabIndex = 1;
            this.BtnMoThuMuc.Text = "Mở thư mục chứa file";
            this.BtnMoThuMuc.UseVisualStyleBackColor = true;
            this.BtnMoThuMuc.Click += new System.EventHandler(this.BtnMoThuMuc_Click);
            // 
            // frMoFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 75);
            this.Controls.Add(this.BtnMoThuMuc);
            this.Controls.Add(this.btnMoFile);
            this.Name = "frMoFile";
            this.Text = "Mở file";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMoFile;
        private System.Windows.Forms.Button BtnMoThuMuc;
    }
}