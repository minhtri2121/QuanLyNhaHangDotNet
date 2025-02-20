using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class FormPhieuNhap : Form
    {
        public FormPhieuNhap()
        {
            InitializeComponent();

            this.Load += FormPhieuNhap_Load;

            IsEntryFormCompleted();

            txtMaPhieuNhap.TextChanged += CheckEntryFormCompletion;

            dtpkNgayNhap.ValueChanged += CheckEntryFormCompletion;

            txtNguoiGiao.TextChanged += CheckEntryFormCompletion;

            txtNhaCungCap.TextChanged += CheckEntryFormCompletion;

            btnHuy.Click += btnHuy_Click;

            btnXacNhan.Click += btnXacNhan_Click;

        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            EnableDetailControls(false);
        }
        private void EnableDetailControls(bool enable)
        {
            txtTenMatHang.Enabled = enable;
            cbLoaiMatHang.Enabled = enable;
            nmSoLuong.Enabled = enable;
            nmGiaNhap.Enabled = enable;
            btnXacNhan.Enabled = enable;
        }
        private bool IsEntryFormCompleted()
        {
            return !string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) &&
                   dtpkNgayNhap.Value != null &&
                   !string.IsNullOrWhiteSpace(txtNguoiGiao.Text) &&
                   !string.IsNullOrWhiteSpace(txtNhaCungCap.Text);
        }
        private void CheckEntryFormCompletion(object sender, EventArgs e)
        {
            if (IsEntryFormCompleted())
            {
                EnableDetailControls(true);  
            }
            else
            {
                EnableDetailControls(false); 
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) &&
                string.IsNullOrWhiteSpace(txtNguoiGiao.Text) &&
                string.IsNullOrWhiteSpace(txtNhaCungCap.Text) &&
                dtpkNgayNhap.Value.Date == DateTime.Now.Date)
            {
                MessageBox.Show( "Bạn chưa nhập gì!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information );
                return; 
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa toàn bộ dữ liệu không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No) return;

            txtMaPhieuNhap.Clear();
            dtpkNgayNhap.Value = DateTime.Now;
            txtNguoiGiao.Clear();
            txtNhaCungCap.Clear();

            txtTenMatHang.Clear();
            cbLoaiMatHang.SelectedIndex = -1;
            nmSoLuong.Value = 0;
            nmGiaNhap.Value = 0;

            EnableDetailControls(false);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
        }
    }
}
