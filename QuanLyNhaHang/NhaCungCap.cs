using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNhaHang
{
    public partial class NhaCungCap : Form
    {
        public NhaCungCap()
        {
            InitializeComponent();
            LoadNCCIntoDtgv();
            AddNCCBinding();
        }

        private void LoadNCCIntoDtgv()
        {
            string query = "Select * From NHA_CUNG_CAP";
            dtgvNCC.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        public event EventHandler AddNCCOn;
        private void btnAddNCC_Click(object sender, EventArgs e)
        {
            string tenNCC = txtTenNCC.Text.Trim();
            string SDT = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "INSERT NHA_CUNG_CAP(TenNCC, DienThoai, DiaChi) Values(N' " + tenNCC + " ', ' " + SDT + " ', N' " + diaChi + " ')";
                DataProvider.Instance.ExcuteQuery(query);
                AddNCCOn?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNCCIntoDtgv();
                AddNCCBinding();

                txtTenNCC.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (txtSDT.Text.Length >= 12 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void txtSDT_Leave(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length < 10 || txtSDT.Text.Length > 12)
            {
                MessageBox.Show("Số điện thoại phải có từ 10 đến 12 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
            }
        }


        private void AddNCCBinding()
        {
            txtIDNCC.DataBindings.Clear();
            txtTenNCC.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();

            txtIDNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "IDNCC", true, DataSourceUpdateMode.Never));
            txtTenNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "TenNCC", true, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "DienThoai", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
        }
        private void btnEditNCC_Click(object sender, EventArgs e)
        {
            string idNCC = txtIDNCC.Text;
            string tenNCC = txtTenNCC.Text;
            string SDT = txtSDT.Text;
            string diaChi = txtDiaChi.Text;

            if (string.IsNullOrEmpty(idNCC) || string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(SDT) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "UPDATE NHA_CUNG_CAP SET TenNCC = N'" + tenNCC + "', DienThoai = '" + SDT + "', DiaChi = N'" + diaChi + "' WHERE IDNCC = " + idNCC;
                DataProvider.Instance.ExcuteQuery(query);
                MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNCCIntoDtgv();
                AddNCCBinding();

                txtIDNCC.Clear();
                txtTenNCC.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();
            }
        }

        private void btnDeleteNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string idNCC = txtIDNCC.Text;
                DataProvider.Instance.ExcuteQuery("DELETE NHA_CUNG_CAP WHERE IDNCC =" + idNCC);
                LoadNCCIntoDtgv();
                AddNCCBinding();

                txtIDNCC.Clear();
                txtTenNCC.Clear();
                txtSDT.Clear();
                txtDiaChi.Clear();
            }
        }
    }
}
