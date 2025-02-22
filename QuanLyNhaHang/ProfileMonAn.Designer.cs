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
            this.btnNhomMon = new System.Windows.Forms.Button();
            this.btnDVT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNhomMon
            // 
            this.btnNhomMon.Location = new System.Drawing.Point(12, 12);
            this.btnNhomMon.Name = "btnNhomMon";
            this.btnNhomMon.Size = new System.Drawing.Size(167, 162);
            this.btnNhomMon.TabIndex = 0;
            this.btnNhomMon.Text = "Nhóm món";
            this.btnNhomMon.UseVisualStyleBackColor = true;
            this.btnNhomMon.Click += new System.EventHandler(this.btnNhomMon_Click);
            // 
            // btnDVT
            // 
            this.btnDVT.Location = new System.Drawing.Point(185, 12);
            this.btnDVT.Name = "btnDVT";
            this.btnDVT.Size = new System.Drawing.Size(167, 162);
            this.btnDVT.TabIndex = 1;
            this.btnDVT.Text = "Loại mặt hàng";
            this.btnDVT.UseVisualStyleBackColor = true;
            this.btnDVT.Click += new System.EventHandler(this.btnDVT_Click);
            // 
            // ProfileMonAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 181);
            this.Controls.Add(this.btnDVT);
            this.Controls.Add(this.btnNhomMon);
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