namespace QuanLyNhaHang
{
    partial class LoaiMatHang
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
            this.dtgvLMH = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtIDmh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteLMH = new System.Windows.Forms.Button();
            this.btnEditLMH = new System.Windows.Forms.Button();
            this.btnAddLMH = new System.Windows.Forms.Button();
            this.txtTenLoaiMH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLMH)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvLMH
            // 
            this.dtgvLMH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvLMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvLMH.Location = new System.Drawing.Point(12, 12);
            this.dtgvLMH.Name = "dtgvLMH";
            this.dtgvLMH.RowHeadersWidth = 51;
            this.dtgvLMH.RowTemplate.Height = 24;
            this.dtgvLMH.Size = new System.Drawing.Size(477, 512);
            this.dtgvLMH.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIDmh);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnDeleteLMH);
            this.panel1.Controls.Add(this.btnEditLMH);
            this.panel1.Controls.Add(this.btnAddLMH);
            this.panel1.Controls.Add(this.txtTenLoaiMH);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(495, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 511);
            this.panel1.TabIndex = 1;
            // 
            // txtIDmh
            // 
            this.txtIDmh.Location = new System.Drawing.Point(45, 43);
            this.txtIDmh.Name = "txtIDmh";
            this.txtIDmh.ReadOnly = true;
            this.txtIDmh.Size = new System.Drawing.Size(237, 22);
            this.txtIDmh.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mã loại:";
            // 
            // btnDeleteLMH
            // 
            this.btnDeleteLMH.Location = new System.Drawing.Point(235, 187);
            this.btnDeleteLMH.Name = "btnDeleteLMH";
            this.btnDeleteLMH.Size = new System.Drawing.Size(81, 44);
            this.btnDeleteLMH.TabIndex = 4;
            this.btnDeleteLMH.Text = "Xoá";
            this.btnDeleteLMH.UseVisualStyleBackColor = true;
            this.btnDeleteLMH.Click += new System.EventHandler(this.btnDeleteLMH_Click);
            // 
            // btnEditLMH
            // 
            this.btnEditLMH.Location = new System.Drawing.Point(122, 187);
            this.btnEditLMH.Name = "btnEditLMH";
            this.btnEditLMH.Size = new System.Drawing.Size(81, 44);
            this.btnEditLMH.TabIndex = 3;
            this.btnEditLMH.Text = "Sửa";
            this.btnEditLMH.UseVisualStyleBackColor = true;
            this.btnEditLMH.Click += new System.EventHandler(this.btnEditLMH_Click);
            // 
            // btnAddLMH
            // 
            this.btnAddLMH.Location = new System.Drawing.Point(12, 187);
            this.btnAddLMH.Name = "btnAddLMH";
            this.btnAddLMH.Size = new System.Drawing.Size(81, 44);
            this.btnAddLMH.TabIndex = 2;
            this.btnAddLMH.Text = "Thêm";
            this.btnAddLMH.UseVisualStyleBackColor = true;
            this.btnAddLMH.Click += new System.EventHandler(this.btnAddLMH_Click);
            // 
            // txtTenLoaiMH
            // 
            this.txtTenLoaiMH.Location = new System.Drawing.Point(45, 109);
            this.txtTenLoaiMH.Name = "txtTenLoaiMH";
            this.txtTenLoaiMH.Size = new System.Drawing.Size(237, 22);
            this.txtTenLoaiMH.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên loại mặt hàng:";
            // 
            // LoaiMatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 526);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtgvLMH);
            this.Name = "LoaiMatHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loại Mặt Hàng";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLMH)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvLMH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddLMH;
        private System.Windows.Forms.Button btnEditLMH;
        private System.Windows.Forms.Button btnDeleteLMH;
        private System.Windows.Forms.TextBox txtTenLoaiMH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDmh;
        private System.Windows.Forms.Label label4;
    }
}