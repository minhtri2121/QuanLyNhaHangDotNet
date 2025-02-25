using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

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

            txtShipper.TextChanged += CheckEntryFormCompletion;

            cbNhaCungCap.TextChanged += CheckEntryFormCompletion;

            btnHuy.Click -= btnHuy_Click;
            btnHuy.Click += btnHuy_Click;

            btnXacNhan.Click -= btnXacNhan_Click;
            btnXacNhan.Click += btnXacNhan_Click;

            LoadNhaCungCap();

            LoadLoaiMatHang();
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
            dtpHsd.Enabled = enable;
        }
        private bool IsEntryFormCompleted()
        {
            return !string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) &&
                   dtpkNgayNhap.Value != null &&
                   !string.IsNullOrWhiteSpace(txtShipper.Text) &&
                   !string.IsNullOrWhiteSpace(cbNhaCungCap.Text);
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

        private void ResetForm()
        {
            txtMaPhieuNhap.Clear();
            dtpkNgayNhap.Value = DateTime.Now;
            txtShipper.Clear();
            cbNhaCungCap.DataSource = null;
            LoadNhaCungCap();
            txtTenMatHang.Clear();
            cbLoaiMatHang.SelectedIndex = -1;
            nmSoLuong.Value = 0;
            nmGiaNhap.Value = 0;

            EnableDetailControls(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text) &&
                string.IsNullOrWhiteSpace(cbNhaCungCap.Text) &&
                dtpkNgayNhap.Value.Date == DateTime.Now.Date &&
                string.IsNullOrWhiteSpace(txtShipper.Text))
            {
                MessageBox.Show("Bạn chưa nhập gì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa toàn bộ dữ liệu không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void LoadNhaCungCap()
        {
            try
            {
                string query = "SELECT IDNCC, TenNCC FROM NHA_CUNG_CAP";
                DataTable dt = DataProvider.Instance.ExcuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    cbNhaCungCap.DataSource = dt;
                    cbNhaCungCap.DisplayMember = "TenNCC";
                    cbNhaCungCap.ValueMember = "IDNCC";
                }
                else
                {
                    cbNhaCungCap.DataSource = null;
                }

                cbNhaCungCap.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadLoaiMatHang()
        {
            string query = "SELECT IDLoaiMH, TenLoaiMH FROM LOAI_MAT_HANG";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);

            cbLoaiMatHang.DataSource = dt;
            cbLoaiMatHang.DisplayMember = "TenLoaiMH"; 
            cbLoaiMatHang.ValueMember = "IDLoaiMH";  
        }


        public event EventHandler XacNhan;
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuNhap.Text))
            {
                MessageBox.Show("Bạn chưa tạo phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenMatHang.Text) || cbLoaiMatHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idPhieuNhap = int.Parse(txtMaPhieuNhap.Text);
            string tenMatHang = txtTenMatHang.Text;
            int idLoaiMatHang = int.Parse(cbLoaiMatHang.SelectedValue.ToString());
            int soLuong = (int)nmSoLuong.Value;
            decimal giaNhap = nmGiaNhap.Value;
            string hanSuDung = dtpHsd.Value.ToString("yyyy-MM-dd");

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm đơn hàng này vào phiếu nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionString))
                {
                    try
                    {
                        connection.Open();

                        string checkQuery = "SELECT IDMatHang FROM MAT_HANG WHERE TenMatHang = @TenMatHang AND IDLoaiMH = @IDLoaiMH AND GiaNhap = @GiaNhap";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                        checkCmd.Parameters.AddWithValue("@TenMatHang", tenMatHang);
                        checkCmd.Parameters.AddWithValue("@IDLoaiMH", idLoaiMatHang);
                        checkCmd.Parameters.AddWithValue("@GiaNhap", giaNhap);

                        object resultQuery = checkCmd.ExecuteScalar();
                        int idMatHang;

                        if (resultQuery != null)
                        {
                            idMatHang = Convert.ToInt32(resultQuery);
                        }
                        else
                        {
                            string insertMatHangQuery = "INSERT INTO MAT_HANG (TenMatHang, GiaNhap, IDLoaiMH, HanSuDung) OUTPUT INSERTED.IDMatHang " +
                                                        "VALUES (@TenMatHang, @GiaNhap, @IDLoaiMH, @HanSuDung)";
                            SqlCommand insertCmd = new SqlCommand(insertMatHangQuery, connection);
                            insertCmd.Parameters.AddWithValue("@TenMatHang", tenMatHang);
                            insertCmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                            insertCmd.Parameters.AddWithValue("@IDLoaiMH", idLoaiMatHang);
                            insertCmd.Parameters.AddWithValue("@HanSuDung", hanSuDung);

                            idMatHang = (int)insertCmd.ExecuteScalar();
                        }

                        string checkCTPNQuery = "SELECT COUNT(*) FROM CTPHIEUNHAP WHERE IDPhieuNhap = @IDPhieuNhap AND IDMatHang = @IDMatHang";
                        SqlCommand checkCTPNCmd = new SqlCommand(checkCTPNQuery, connection);
                        checkCTPNCmd.Parameters.AddWithValue("@IDPhieuNhap", idPhieuNhap);
                        checkCTPNCmd.Parameters.AddWithValue("@IDMatHang", idMatHang);

                        int count = (int)checkCTPNCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            string updateQuery = "UPDATE CTPHIEUNHAP " +
                                                  "SET SoLuong = SoLuong + @SoLuong, DonGia = @DonGia * (SoLuong + @SoLuong) " +
                                                  "WHERE IDPhieuNhap = @IDPhieuNhap AND IDMatHang = @IDMatHang";

                            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                            updateCmd.Parameters.AddWithValue("@IDPhieuNhap", idPhieuNhap);
                            updateCmd.Parameters.AddWithValue("@IDMatHang", idMatHang);
                            updateCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            updateCmd.Parameters.AddWithValue("@DonGia", giaNhap);

                            updateCmd.ExecuteNonQuery();
                            MessageBox.Show("Cập nhật số lượng mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string insertCTPNQuery = "INSERT INTO CTPHIEUNHAP (IDPhieuNhap, IDMatHang, SoLuong, DonGia) " +
                                                    "VALUES (@IDPhieuNhap, @IDMatHang, @SoLuong, @DonGia)";

                            SqlCommand insertCTPNCmd = new SqlCommand(insertCTPNQuery, connection);
                            insertCTPNCmd.Parameters.AddWithValue("@IDPhieuNhap", idPhieuNhap);
                            insertCTPNCmd.Parameters.AddWithValue("@IDMatHang", idMatHang);
                            insertCTPNCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            insertCTPNCmd.Parameters.AddWithValue("@DonGia", giaNhap * soLuong);

                            insertCTPNCmd.ExecuteNonQuery();
                            MessageBox.Show("Thêm mặt hàng vào phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                XacNhan?.Invoke(this, EventArgs.Empty);
                LoadShowEntryFormList(idPhieuNhap);
            }
        }

        private void btnTaoPN_Click(object sender, EventArgs e)
        {
            string ngayNhap = dtpkNgayNhap.Value.ToString("yyyy-MM-dd");
            string nguoiGiao = txtShipper.Text;

            if (cbNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idNCC = int.Parse(cbNhaCungCap.SelectedValue.ToString());

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "INSERT INTO PHIEU_NHAP (NgayLapPN, NguoiGiao, IDNCC) OUTPUT INSERTED.IDPhieuNhap VALUES (@NgayLapPN, @NguoiGiao, @IDNCC)";

                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@NgayLapPN", ngayNhap);
                            command.Parameters.AddWithValue("@NguoiGiao", nguoiGiao);
                            command.Parameters.AddWithValue("@IDNCC", idNCC);

                            object resultQuery = command.ExecuteScalar();

                            if (resultQuery != null)
                            {
                                txtMaPhieuNhap.Text = resultQuery.ToString();
                                MessageBox.Show("Tạo phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                EnableDetailControls(true);
                            }
                            else
                            {
                                MessageBox.Show("Tạo phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                EnableDetailControls(false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                int idPhieuNhap = Convert.ToInt32(txtMaPhieuNhap.Text);
                LoadShowEntryFormList(idPhieuNhap);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProfilePN profile = new ProfilePN();
            NhaCungCap f = new NhaCungCap();
            profile.ShowDialog();
        }

        void LoadShowEntryFormList(int idPhieuNhap)
        {
            List<DTO.ShowEntryForm> showEntryForms = ShowEntryFormDAO.Instance.LoadShowEntryFormList(idPhieuNhap);
            dtgvHienThiPhieuNhap.DataSource = showEntryForms;
        }
    }
}
