using System;
using System.Data;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class DonViTinh : Form
    {
        public DonViTinh()
        {
            InitializeComponent();
            LoadDVTIntoDtgv();
        }

        private void LoadDVTIntoDtgv()
        {
            string query = "SELECT IDDVT as [Mã đơn vị tính], TenDVT as [Tên đơn vị tính] From DON_VI_TINH";
            dtgvDVT.DataSource = DataProvider.Instance.ExcuteQuery(query);
            AddDVTBinding();
        }

        private void AddDVTBinding()
        {
            txtID.DataBindings.Clear();
            txtName.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", dtgvDVT.DataSource, "Mã đơn vị tính", true, DataSourceUpdateMode.Never));
            txtName.DataBindings.Add(new Binding("Text", dtgvDVT.DataSource, "Tên đơn vị tính", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên đơn vị tính không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string q = "INSERT INTO DON_VI_TINH (TenDVT) VALUES (N'" + name + "')";
            DataProvider.Instance.ExcuteQuery(q);
            LoadDVTIntoDtgv();
            MessageBox.Show("Thêm đơn vị tính thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Tên đơn vị tính không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string q = "UPDATE DON_VI_TINH SET TenDVT = N'" + name + "' WHERE IDDVT = " + id;
            DataProvider.Instance.ExcuteQuery(q);
            LoadDVTIntoDtgv();
            MessageBox.Show("Cập nhật đơn vị tính thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn vị tính này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            string q = "DELETE FROM DON_VI_TINH WHERE IDDVT = " + id;
            DataProvider.Instance.ExcuteQuery(q);
            LoadDVTIntoDtgv();
            MessageBox.Show("Xóa đơn vị tính thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
