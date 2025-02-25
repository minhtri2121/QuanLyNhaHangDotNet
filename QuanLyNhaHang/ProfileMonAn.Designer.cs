namespace QuanLyNhaHang
{
    partial class ProfileMonAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileMonAn));
            this.btnNhomMon = new System.Windows.Forms.Button();
            this.btnDVT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNhomMon
            // 
            this.btnNhomMon.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNhomMon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNhomMon.Location = new System.Drawing.Point(12, 12);
            this.btnNhomMon.Name = "btnNhomMon";
            this.btnNhomMon.Size = new System.Drawing.Size(167, 162);
            this.btnNhomMon.TabIndex = 0;
            this.btnNhomMon.Text = "Nhóm món";
            this.btnNhomMon.UseVisualStyleBackColor = false;
            this.btnNhomMon.Click += new System.EventHandler(this.btnNhomMon_Click);
            // 
            // btnDVT
            // 
            this.btnDVT.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDVT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDVT.Location = new System.Drawing.Point(185, 12);
            this.btnDVT.Name = "btnDVT";
            this.btnDVT.Size = new System.Drawing.Size(167, 162);
            this.btnDVT.TabIndex = 1;
            this.btnDVT.Text = "Đơn vị tính";
            this.btnDVT.UseVisualStyleBackColor = false;
            this.btnDVT.Click += new System.EventHandler(this.btnDVT_Click);
            // 
            // ProfileMonAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(360, 181);
            this.Controls.Add(this.btnDVT);
            this.Controls.Add(this.btnNhomMon);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileMonAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfilePN";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNhomMon;
        private System.Windows.Forms.Button btnDVT;
    }
}