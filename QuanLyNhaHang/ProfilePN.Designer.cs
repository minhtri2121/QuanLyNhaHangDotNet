namespace QuanLyNhaHang
{
    partial class ProfilePN
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
            this.btnNCC = new System.Windows.Forms.Button();
            this.btnLoaiMH = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNCC
            // 
            this.btnNCC.Location = new System.Drawing.Point(12, 12);
            this.btnNCC.Name = "btnNCC";
            this.btnNCC.Size = new System.Drawing.Size(167, 162);
            this.btnNCC.TabIndex = 0;
            this.btnNCC.Text = "Nhà cung cấp";
            this.btnNCC.UseVisualStyleBackColor = true;
            this.btnNCC.Click += new System.EventHandler(this.btnNCC_Click);
            // 
            // btnLoaiMH
            // 
            this.btnLoaiMH.Location = new System.Drawing.Point(185, 12);
            this.btnLoaiMH.Name = "btnLoaiMH";
            this.btnLoaiMH.Size = new System.Drawing.Size(167, 162);
            this.btnLoaiMH.TabIndex = 1;
            this.btnLoaiMH.Text = "Loại mặt hàng";
            this.btnLoaiMH.UseVisualStyleBackColor = true;
            // 
            // ProfilePN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 181);
            this.Controls.Add(this.btnLoaiMH);
            this.Controls.Add(this.btnNCC);
            this.Name = "ProfilePN";
            this.Text = "ProfilePN";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNCC;
        private System.Windows.Forms.Button btnLoaiMH;
    }
}