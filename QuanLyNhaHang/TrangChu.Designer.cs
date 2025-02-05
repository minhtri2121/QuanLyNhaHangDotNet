namespace QuanLyNhaHang
{
    partial class TrangChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrangChu));
            this.ptbTrangChu = new System.Windows.Forms.PictureBox();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnThucDon = new System.Windows.Forms.Button();
            this.btnBan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ptbTrangChu)).BeginInit();
            this.SuspendLayout();
            // 
            // ptbTrangChu
            // 
            this.ptbTrangChu.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ptbTrangChu.Location = new System.Drawing.Point(12, 12);
            this.ptbTrangChu.Name = "ptbTrangChu";
            this.ptbTrangChu.Size = new System.Drawing.Size(220, 113);
            this.ptbTrangChu.TabIndex = 0;
            this.ptbTrangChu.TabStop = false;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Location = new System.Drawing.Point(12, 131);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(220, 56);
            this.btnTrangChu.TabIndex = 1;
            this.btnTrangChu.Text = "Trang Chủ";
            this.btnTrangChu.UseVisualStyleBackColor = true;
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Location = new System.Drawing.Point(12, 193);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(220, 56);
            this.btnDanhMuc.TabIndex = 2;
            this.btnDanhMuc.Text = "Danh Mục";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            // 
            // btnThucDon
            // 
            this.btnThucDon.Location = new System.Drawing.Point(12, 317);
            this.btnThucDon.Name = "btnThucDon";
            this.btnThucDon.Size = new System.Drawing.Size(220, 56);
            this.btnThucDon.TabIndex = 3;
            this.btnThucDon.Text = "Thực Đơn";
            this.btnThucDon.UseVisualStyleBackColor = true;
            // 
            // btnBan
            // 
            this.btnBan.Location = new System.Drawing.Point(12, 255);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(220, 56);
            this.btnBan.TabIndex = 4;
            this.btnBan.Text = "Bàn";
            this.btnBan.UseVisualStyleBackColor = true;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 541);
            this.Controls.Add(this.btnBan);
            this.Controls.Add(this.btnThucDon);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnTrangChu);
            this.Controls.Add(this.ptbTrangChu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrangChu";
            this.Text = "Trang Chủ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ptbTrangChu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ptbTrangChu;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnThucDon;
        private System.Windows.Forms.Button btnBan;
    }
}

