namespace QuanLyNhaHang
{
    partial class NhaCungCap
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
            this.dtgvNCC = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtIDNCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteNCC = new System.Windows.Forms.Button();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.btnEditNCC = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNCC = new System.Windows.Forms.Button();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvNCC)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvNCC
            // 
            this.dtgvNCC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvNCC.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtgvNCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvNCC.Location = new System.Drawing.Point(12, 12);
            this.dtgvNCC.Name = "dtgvNCC";
            this.dtgvNCC.RowHeadersWidth = 51;
            this.dtgvNCC.RowTemplate.Height = 24;
            this.dtgvNCC.Size = new System.Drawing.Size(477, 512);
            this.dtgvNCC.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIDNCC);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnDeleteNCC);
            this.panel1.Controls.Add(this.txtDiaChi);
            this.panel1.Controls.Add(this.btnEditNCC);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnAddNCC);
            this.panel1.Controls.Add(this.txtSDT);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTenNCC);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(495, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 511);
            this.panel1.TabIndex = 1;
            // 
            // txtIDNCC
            // 
            this.txtIDNCC.Location = new System.Drawing.Point(45, 43);
            this.txtIDNCC.Name = "txtIDNCC";
            this.txtIDNCC.ReadOnly = true;
            this.txtIDNCC.Size = new System.Drawing.Size(237, 22);
            this.txtIDNCC.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mã nhà cung cấp:";
            // 
            // btnDeleteNCC
            // 
            this.btnDeleteNCC.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeleteNCC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteNCC.Location = new System.Drawing.Point(235, 315);
            this.btnDeleteNCC.Name = "btnDeleteNCC";
            this.btnDeleteNCC.Size = new System.Drawing.Size(81, 44);
            this.btnDeleteNCC.TabIndex = 5;
            this.btnDeleteNCC.Text = "Xoá";
            this.btnDeleteNCC.UseVisualStyleBackColor = false;
            this.btnDeleteNCC.Click += new System.EventHandler(this.btnDeleteNCC_Click);
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(45, 239);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(237, 22);
            this.txtDiaChi.TabIndex = 2;
            // 
            // btnEditNCC
            // 
            this.btnEditNCC.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEditNCC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditNCC.Location = new System.Drawing.Point(122, 315);
            this.btnEditNCC.Name = "btnEditNCC";
            this.btnEditNCC.Size = new System.Drawing.Size(81, 44);
            this.btnEditNCC.TabIndex = 4;
            this.btnEditNCC.Text = "Sửa";
            this.btnEditNCC.UseVisualStyleBackColor = false;
            this.btnEditNCC.Click += new System.EventHandler(this.btnEditNCC_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Địa chỉ:";
            // 
            // btnAddNCC
            // 
            this.btnAddNCC.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddNCC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNCC.Location = new System.Drawing.Point(12, 315);
            this.btnAddNCC.Name = "btnAddNCC";
            this.btnAddNCC.Size = new System.Drawing.Size(81, 44);
            this.btnAddNCC.TabIndex = 3;
            this.btnAddNCC.Text = "Thêm";
            this.btnAddNCC.UseVisualStyleBackColor = false;
            this.btnAddNCC.Click += new System.EventHandler(this.btnAddNCC_Click);
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(45, 174);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(237, 22);
            this.txtSDT.TabIndex = 1;
            this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSDT_KeyPress);
            this.txtSDT.Leave += new System.EventHandler(this.txtSDT_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số điện thoại:";
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Location = new System.Drawing.Point(45, 109);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(237, 22);
            this.txtTenNCC.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên nhà cung cấp:";
            // 
            // NhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(828, 526);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtgvNCC);
            this.Name = "NhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NhaCungCap";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvNCC)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvNCC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddNCC;
        private System.Windows.Forms.Button btnEditNCC;
        private System.Windows.Forms.Button btnDeleteNCC;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDNCC;
        private System.Windows.Forms.Label label4;
    }
}