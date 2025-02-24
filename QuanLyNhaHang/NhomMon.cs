using System;
using System.Data;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class NhomMon : Form
    {
        public NhomMon()
        {
            InitializeComponent();
            LoadNhomMonIntoDtgv();
        }

        private void LoadNhomMonIntoDtgv()
        {
            string query = "SELECT IDNhomMon as [Mã nhóm món], TenNhomMon as [Tên nhóm món] From NHOM_MON";
            dtgvNhomMon.DataSource = DataProvider.Instance.ExcuteQuery(query);
            AddNhomMonBinding();
        }

        private void AddNhomMonBinding()
        {
            txtID.DataBindings.Clear();
            txtName.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", dtgvNhomMon.DataSource, "Mã nhóm món", true, DataSourceUpdateMode.Never));
            txtName.DataBindings.Add(new Binding("Text", dtgvNhomMon.DataSource, "Tên nhóm món", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên nhóm món không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhóm món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "INSERT NHOM_MON (TenNhomMon) VALUES( N'" + name + "' )";
                DataProvider.Instance.ExcuteQuery(query);
                LoadNhomMonIntoDtgv();
                MessageBox.Show("Thêm nhóm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên nhóm món không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật nhóm món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "UPDATE NHOM_MON SET TenNhomMon = N'" + name + "' WHERE IDNhomMon = " + id;
                DataProvider.Instance.ExcuteQuery(query);
                LoadNhomMonIntoDtgv();
                MessageBox.Show("Cập nhật nhóm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm món này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE NHOM_MON WHERE IDNhomMon = " + id;
                DataProvider.Instance.ExcuteQuery(query);
                LoadNhomMonIntoDtgv();
                MessageBox.Show("Xóa nhóm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
