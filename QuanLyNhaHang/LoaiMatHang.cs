using System;
using System.Data;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class LoaiMatHang : Form
    {
        public LoaiMatHang()
        {
            InitializeComponent();
            LoadMatIntoDTGV();
        }

        private void LoadMatIntoDTGV()
        {
            string query = "SELECT * FROM Loai_Mat_Hang";
            dtgvLMH.DataSource = DataProvider.Instance.ExcuteQuery(query);
            AddLMHBinding();
        }

        private void AddLMHBinding()
        {
            txtIDmh.DataBindings.Clear();
            txtTenLoaiMH.DataBindings.Clear();

            txtIDmh.DataBindings.Add(new Binding("Text", dtgvLMH.DataSource, "IDLoaiMH", true, DataSourceUpdateMode.Never));
            txtTenLoaiMH.DataBindings.Add(new Binding("Text", dtgvLMH.DataSource, "TenLoaiMH", true, DataSourceUpdateMode.Never));
        }

        private void btnAddLMH_Click(object sender, EventArgs e)
        {
            string tenLoai = txtTenLoaiMH.Text.Trim();

            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại mặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm loại mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "INSERT INTO LOAI_MAT_HANG (TenLoaiMH) VALUES (N'" + tenLoai + "')";
                    DataProvider.Instance.ExcuteQuery(query);
                    MessageBox.Show("Thêm loại mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMatIntoDTGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm loại mặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditLMH_Click(object sender, EventArgs e)
        {
            string tenLoai = txtTenLoaiMH.Text.Trim();

            if (!int.TryParse(txtIDmh.Text, out int idMH))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại mặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật loại mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "UPDATE LOAI_MAT_HANG SET TenLoaiMH = N'" + tenLoai + "' WHERE IDLoaiMH = " + idMH;
                    DataProvider.Instance.ExcuteQuery(query);
                    MessageBox.Show("Cập nhật loại mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMatIntoDTGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật loại mặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteLMH_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIDmh.Text, out int idMH))
            {
                MessageBox.Show("ID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa loại mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM LOAI_MAT_HANG WHERE IDLoaiMH = " + idMH;
                    DataProvider.Instance.ExcuteQuery(query);
                    MessageBox.Show("Xóa loại mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMatIntoDTGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa loại mặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
