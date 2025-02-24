using System;
using System.Data;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class KhuVuc : Form
    {
        public KhuVuc()
        {
            InitializeComponent();
            LoadKVIntoDtgv();
        }

        private void LoadKVIntoDtgv()
        {
            string q = "SELECT IDKhuVuc as [Mã khu vực], TenKhuVuc as [Tên khu vực] FROM KHU_VUC";
            dtgvKhuVuc.DataSource = DataProvider.Instance.ExcuteQuery(q);
            AddKVBinding();
        }

        private void AddKVBinding()
        {
            txtID.DataBindings.Clear();
            txtName.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", dtgvKhuVuc.DataSource, "Mã khu vực", true, DataSourceUpdateMode.Never));
            txtName.DataBindings.Add(new Binding("Text", dtgvKhuVuc.DataSource, "Tên khu vực", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên khu vực không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm khu vực này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string q = "INSERT INTO KHU_VUC (TenKhuVuc) VALUES (N'" + name + "')";
                DataProvider.Instance.ExcuteQuery(q);
                LoadKVIntoDtgv();
                MessageBox.Show("Thêm khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Tên khu vực không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật khu vực này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string q = "UPDATE KHU_VUC SET TenKhuVuc = N'" + name + "' WHERE IDKhuVuc = " + id;
                DataProvider.Instance.ExcuteQuery(q);
                LoadKVIntoDtgv();
                MessageBox.Show("Cập nhật khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khu vực này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string q = "DELETE FROM KHU_VUC WHERE IDKhuVuc = " + id;
                DataProvider.Instance.ExcuteQuery(q);
                LoadKVIntoDtgv();
                MessageBox.Show("Xóa khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
