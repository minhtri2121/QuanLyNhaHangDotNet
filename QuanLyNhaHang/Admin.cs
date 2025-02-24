﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using System.Xml.Serialization;
using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        BindingSource FoodList = new BindingSource();

        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();

            LoadDateTimePickerBill();

            LoadDoanhThuByDate(dtpStart.Value.ToString("yyyy-MM-dd"), dtpEnd.Value.ToString("yyyy-MM-dd"));

            LoadAdmin();
        }

        #region methods

        private void LoadAdmin()
        {
            dtgvMonAn.DataSource = FoodList;

            dtgvTaiKhoan.DataSource = accountList;

            LoadListFood();

            LoadAccount();

            LoadCategoryIntoComboBox(cbNhomMon);

            LoadTableStatus(cbKhuVuc);

            LoadDVTIntoComboBox(cbDVT);

            AddFoddBinding();

            AddAccountBinding();

            LoadComboBoxTable(cbBan);

            LoadListAccIntoComboBox(cbTenNguoiDung);

            LoadBanAn();

            AddTableFood();

            LoadListEntryForm();

            dtgvPhieuNhap.CellClick += dtgvPhieuNhap_CellClick;

            this.btnTimPN.Click += new System.EventHandler(this.btnTimPN_Click);

            this.btnTimPN.Click -= new System.EventHandler(this.btnTimPN_Click);

            this.txtNhaCungCap.KeyDown += new KeyEventHandler(this.txtMaPhieuNhap_KeyDown);

        }

        void AddAccountBinding()
        {
            txtIDNguoiDung.DataBindings.Clear();
            txtTenTk.DataBindings.Clear();
            txtTenHienThi.DataBindings.Clear();
            nmLoaiTK.DataBindings.Clear();

            txtIDNguoiDung.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "IDNguoiDung", true, DataSourceUpdateMode.Never));
            txtTenTk.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenDangNhap", true, DataSourceUpdateMode.Never));
            txtTenHienThi.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenNguoiDung", true, DataSourceUpdateMode.Never));
            nmLoaiTK.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "Admin", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void LoadListFood()
        {
            string query = "SELECT IDMon, TuKhoa, TenMon, TenNhomMon , TenDVT,FORMAT ( Gia ,'0') AS Gia\r\nFROM MON \r\nJOIN NHOM_MON ON MON.IDNhomMon = NHOM_MON.IDNhomMon\r\nJOIN DON_VI_TINH ON MON.IDDVT = DON_VI_TINH.IDDVT;";

            FoodList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }

        void EditAccount(string @userName, string @displayName, int @type, int id )
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type, id))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.tenDangNhap.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xoá chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xoá tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại");
            }

            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }

        void AddFoddBinding()
        {
            txtIDMon.DataBindings.Clear();
            txtTenMon.DataBindings.Clear();
            txtTuKhoa.DataBindings.Clear();
            cbNhomMon.DataBindings.Clear();
            cbDVT.DataBindings.Clear();
            nmGia.DataBindings.Clear();

            txtIDMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "IDMon", true, DataSourceUpdateMode.Never));
            txtTenMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenMon", true, DataSourceUpdateMode.Never));
            txtTuKhoa.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TuKhoa", true, DataSourceUpdateMode.Never));
            cbNhomMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenNhomMon", true, DataSourceUpdateMode.Never));
            cbDVT.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenDVT", true, DataSourceUpdateMode.Never));
            nmGia.DataBindings.Add(new Binding("Value", dtgvMonAn.DataSource, "Gia", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetCategory();
            cb.DisplayMember = "Name";
        }

        void LoadTableStatus(ComboBox cb)
        {
            cb.DataSource = ZoneDAO.Instance.GetTableListStatus();
            cb.DisplayMember = "name";
        }

        void LoadDVTIntoComboBox(ComboBox cb)
        {
            cb.DataSource = FoodDAO.Instance.GetDVT();
            cb.DisplayMember = "Name";
        }

        void LoadListAccIntoComboBox(ComboBox cb)
        {
            List<Account> accounts = AccountDAO.Instance.GetListAccounts();

            accounts.Insert(0, item: new Account { TenNguoiDung = "-- Chọn người dùng --" });

            cb.DataSource = accounts;
            cb.DisplayMember = "tenNguoiDung";

            cb.SelectedIndex = 0;
        }

        void LoadComboBoxTable(ComboBox cb)
        {
            List<Table> table = TableDAO.Instance.LoadTableList();

            table.Insert(0, item: new Table { Name = "-- Chọn Bàn --" });

            cb.DataSource = table;
            cb.DisplayMember = "Name";
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;

            dtpStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);
        }

        void LoadDoanhThuByDate(string date1, string date2)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillByDate(date1, date2);
        }

        void LoadDoanhThuByNguoiDung(string date1, string date2, string nguoidung)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillByNguoiDung(date1, date2, nguoidung);
        }

        void LoadDoanhThuByBan(string date1, string date2, string ban)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillByTenBan(date1, date2, ban);
        }

        void LoadDoanhThuByAll(string date1, string date2, string ban, string nguoiDung)
        {
            dtgvDoanhThu.DataSource = BillDAO.Instance.GetBillByAll(date1, date2, ban, nguoiDung);
        }


        void LoadBanAn()
        {
            dtgvBanAn.DataSource = TableDAO.Instance.GetTableList();
        }

        void LoadListEntryForm()
        {
            List<DTO.EntryForm> entryFormList = EntryFormDAO.Instance.LoadEntryFormList(); 
            dtgvPhieuNhap.DataSource = entryFormList;
        }

        void AddTableFood()
        {
            txtIDTableName.DataBindings.Clear();
            txtNameTable.DataBindings.Clear();
            cbKhuVuc.DataBindings.Clear();
            txtTableStatus.DataBindings.Clear();


            txtIDTableName.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameTable.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbKhuVuc.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Zone", true, DataSourceUpdateMode.Never));
            txtTableStatus.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }

        void GetListWarehouseByDate(string date1, string date2)
        {
            dtgvKho.DataSource = WarehouseDAO.Instance.GetListWarehouseByDate(date1, date2);
        }
        #endregion

        #region Event
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
            int? idnhommon = (cbNhomMon.SelectedItem as Category)?.Id;
            int? iddvt = (cbDVT.SelectedItem as DVT)?.Id;
            float Gia = (float)nmGia.Value;

            if ((int)Gia > 0 && idnhommon != null && iddvt != null)
            {
                FoodDAO.Instance.InsertFood(tukhoa, tenmon, iddvt, idnhommon, (int)Gia);
                MessageBox.Show("Thêm món ăn thành công.");
                LoadListFood();
            }
            else
            if ((cbNhomMon.SelectedItem as Category)?.Id == null || (cbDVT.SelectedItem as DVT)?.Id == null || (int)Gia <= 0)
            {
                string loi1 = "Lỗi, Vui lòng chọn đúng nhóm món";

                string loi2 = "Lỗi, Vui lòng chọn đúng đơn vị tính";

                string loi3 = "Lỗi, Giá nhỏ hơn hoặc bằng 0";

                MessageBox.Show("Có lỗi khi Thêm món ăn.\n\nCó thể bạn đã sai khi nhập nhóm món và đơn vị tính hoặc giá!\n\nNhóm món: " + loi1 + "\n\nĐơn vị tính: " + loi2 + "\n\nGiá: " + loi3);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
            int? idnhommon = (cbNhomMon.SelectedItem as Category)?.Id;
            int? iddvt = (cbDVT.SelectedItem as DVT)?.Id;
            float Gia = (float)nmGia.Value;
            int idmon = Convert.ToInt32(txtIDMon.Text);

            if ((int)Gia > 0 && idnhommon != null && iddvt != null)
            {
                FoodDAO.Instance.UpdateFood(idmon, tukhoa, tenmon, iddvt, idnhommon, (int)Gia);
                MessageBox.Show("Sửa món ăn thành công.");
                LoadListFood();
                LoadAdmin();
            }
            else
            {
                if ((cbNhomMon.SelectedItem as Category)?.Id == null || (cbDVT.SelectedItem as DVT)?.Id == null || (int)Gia <= 0)
                {
                    string loi1 = "Lỗi, Vui lòng chọn đúng nhóm món";

                    string loi2 = "Lỗi, Vui lòng chọn đúng đơn vị tính";

                    string loi3 = "Lỗi, Giá nhỏ hơn hoặc bằng 0";

                    MessageBox.Show("Có lỗi khi Sửa món ăn.\n\nCó thể bạn đã sai khi nhập nhóm món và đơn vị tính hoặc giá!\n\nNhóm món: " + loi1 + "\n\nĐơn vị tính: " + loi2 + "\n\nGiá: " + loi3);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvMonAn.Controls.Clear();
            List<SearchFood> listSearchFood = FoodDAO.Instance.SearchFood(txtScFoodName.Text);

            if (listSearchFood != null && listSearchFood.Count > 0)
            {
                dtgvMonAn.DataSource = typeof(List<SearchFood>);
                dtgvMonAn.DataSource = listSearchFood;
                LoadListFood();
                AddFoddBinding();
            }
            else
            {
                MessageBox.Show("Không tìm thấy món ăn nào.");
            }
        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            string displayName = txtTenHienThi.Text;
            int type = (int)nmLoaiTK.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            DeleteAccount(userName);
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDNguoiDung.Text);
            string userName = txtTenTk?.Text?.ToString();
            string displayName = txtTenHienThi.Text;
            int type = (int)nmLoaiTK.Value;

            EditAccount(userName, displayName, type, id);
        }

        private void btnRePass_Click(object sender, EventArgs e)
        {
            string userName = txtTenTk.Text;
            ResetPass(userName);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadAdmin();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            string firstDateStr = dtpStart.Value.ToString("yyyy-MM-dd");
            string finalDateStr = dtpEnd.Value.ToString("yyyy-MM-dd");

            DataTable dt = BillDAO.Instance.GetBillByDate(firstDateStr, finalDateStr);

            string nguoiDung = cbTenNguoiDung.SelectedIndex > 0 ? cbTenNguoiDung.Text : "";
            string tenBan = cbBan.SelectedIndex > 0 ? cbBan.Text : "";

            string caseValue = (string.IsNullOrEmpty(tenBan) ? "0" : "1") + (string.IsNullOrEmpty(nguoiDung) ? "0" : "2");

            switch (caseValue)
            {
                case "00":
                    LoadDoanhThuByDate(firstDateStr, finalDateStr);
                    break;

                case "10":
                    LoadDoanhThuByBan(firstDateStr, finalDateStr, tenBan);
                    break;

                case "02":
                    LoadDoanhThuByNguoiDung(firstDateStr, finalDateStr, nguoiDung);
                    break;

                case "12":
                    LoadDoanhThuByAll(firstDateStr, finalDateStr, tenBan, nguoiDung);
                    break;

                default:
                    MessageBox.Show("Lựa chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void btnXemTableFood_Click(object sender, EventArgs e)
        {
            LoadBanAn();
        }

        public event EventHandler OnTableAdded;
        private void btnAddTableFood_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int idzone = (cbKhuVuc.SelectedItem as Zone).Id;
            if (TableDAO.instance.InsertTableFood(name, idzone))
            {
                MessageBox.Show("Thêm bàn thành công");
                LoadBanAn();
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại");
            }
            LoadAdmin();
            OnTableAdded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnTableUpdated;
        private void btnEditTableFood_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int? idzone = (cbKhuVuc.SelectedItem as Zone)?.Id;
            if (idzone == null)
            {
                MessageBox.Show("Vui lòng chọn khu vực");
                return;
            }
            int id = Convert.ToInt32(txtIDTableName.Text);
            if (TableDAO.instance.UpdateTableFood(id, name, idzone))
            {
                MessageBox.Show("Sửa bàn thành công");
                LoadBanAn();
            }
            else
            {
                MessageBox.Show("Sửa bàn thất bại");
            }

            LoadAdmin();
            OnTableUpdated?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnTableDeleted;
        private void btnDeleteTableFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDTableName.Text);
            string status = TableDAO.Instance.GetTableByStatus(id);
            if (status == "Có Người")
            {
                MessageBox.Show("Bàn đang có người, không thể xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TableDAO.instance.DeleteTableFood(id))
            {
                MessageBox.Show("Xoá bàn thành công");
                LoadBanAn();
                OnTableDeleted?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Xoá bàn thất bại");
            }
            LoadAdmin();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra ID có nhập hay không
            if (string.IsNullOrWhiteSpace(txtIDMon.Text))
            {
                MessageBox.Show("Vui lòng nhập ID món cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra ID có hợp lệ không (phải là số)
            if (!int.TryParse(txtIDMon.Text, out int id))
            {
                MessageBox.Show("ID món không hợp lệ. Vui lòng nhập số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Kiểm tra xem ID món có tồn tại trong database hay không
            string checkQuery = "SELECT COUNT(*) FROM MON WHERE IDMon = @ID";
            int count = (int)DataProvider.Instance.ExcuteNonScalar(checkQuery, new object[] { id });

            if (count == 0)
            {
                MessageBox.Show("Không tìm thấy món có ID này để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Xác nhận xóa
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa món có ID {id} không?",
                                                  "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            // 5. Thực hiện xóa món ăn
            try
            {
                string deleteQuery = "DELETE FROM MON WHERE IDMon = @ID";
                int rowsAffected = DataProvider.Instance.ExcuteNonQuery(deleteQuery, new object[] { id });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa món thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListFood(); // Cập nhật lại danh sách món
                }
                else
                {
                    MessageBox.Show("Không thể xóa món này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnTraCuuKho_Click(object sender, EventArgs e)
        {
            string date1 = dtpKho1.Value.ToString("yyyy-MM-dd");
            string date2 = dtpKho2.Value.ToString("yyyy-MM-dd");

            GetListWarehouseByDate(date1, date2);
            LoadAdmin();
        }

        private void dtgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dtgvPhieuNhap.Rows[e.RowIndex];
                int? idPhieuNhap = DBNull.Value.Equals(row.Cells["MaPhieuNhap"].Value) ? (int?) null : Convert.ToInt32(row.Cells["MaPhieuNhap"].Value);

                if (idPhieuNhap > 0)
                {
                    LoadDetailEntryForm(idPhieuNhap);
                }
            }
        }
        private void LoadDetailEntryForm(int? maPhieuNhap)
        {
            string query = "SELECT MAT_HANG.TenMatHang, LOAI_MAT_HANG.TenLoaiMH, CTPHIEUNHAP.SoLuong, MAT_HANG.GiaNhap " +
                           "FROM CTPHIEUNHAP " +
                           "JOIN MAT_HANG ON CTPHIEUNHAP.IDMatHang = MAT_HANG.IDMatHang " +
                           "JOIN LOAI_MAT_HANG ON MAT_HANG.IDLoaiMH = LOAI_MAT_HANG.IDLoaiMH " +
                           "WHERE CTPHIEUNHAP.IDPhieuNhap = " + maPhieuNhap;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            dtgvChiTietPhieuNhap.DataSource = data;
        }

        #endregion


        private void btnTimPN_Click(object sender, EventArgs e)
        {
            string nhaCungCap = txtNhaCungCap.Text.Trim();

            if (string.IsNullOrEmpty(nhaCungCap))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu nhập cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT PHIEU_NHAP.IDPhieuNhap AS MaPhieuNhap, PHIEU_NHAP.NgayLapPN AS NgayNhap,PHIEU_NHAP.NguoiGiao AS NguoiGiao, NHA_CUNG_CAP.TenNCC AS NhaCungCap FROM PHIEU_NHAP JOIN NHA_CUNG_CAP ON NHA_CUNG_CAP.IDNCC = PHIEU_NHAP.IDNCC WHERE NHA_CUNG_CAP.TenNCC LIKE @TenNCC";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { "%" + nhaCungCap + "%" });

            if (data.Rows.Count > 0)
            {
                dtgvPhieuNhap.DataSource = data;
            }
            else
            {
                MessageBox.Show("Không tìm thấy phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtgvPhieuNhap.DataSource = null;
            }
        } 
        private void txtMaPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimPN.PerformClick(); 
            }
        }

        private void btnTaoPhieuNhap_Click(object sender, EventArgs e)
        {
            FormPhieuNhap f = new FormPhieuNhap();
            f.XacNhan += F_XacNhan;
            f.ShowDialog();
        }

        private void F_XacNhan(object sender, EventArgs e)
        {
            LoadAdmin();
        }

        private void btnTuyChon_Click(object sender, EventArgs e)
        {
            ProfileMonAn f = new ProfileMonAn();
            f.ShowDialog();
        }

        private void bntAddKV_Click(object sender, EventArgs e)
        {
            KhuVuc f = new KhuVuc();
            f.ShowDialog();
        }
    }
}
