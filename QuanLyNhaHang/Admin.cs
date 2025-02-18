using System;
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

            LoadQuanLiKho();

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

            AddCategoryBinding();
        }

        void AddAccountBinding()
        {
            txtTenTk.DataBindings.Clear();
            txtTenHienThi.DataBindings.Clear();
            nmLoaiTK.DataBindings.Clear();

            txtTenTk.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenDangNhap",true, DataSourceUpdateMode.Never));
            txtTenHienThi.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenNguoiDung", true, DataSourceUpdateMode.Never));
            nmLoaiTK.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "Admin", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txtMatHangID.DataBindings.Clear();
            txtTenMatHang.DataBindings.Clear();
            nmGiaNhap.DataBindings.Clear();
            txtHSD.DataBindings.Clear();
            cbLoaiMH.DataBindings.Clear();

            txtMatHangID.DataBindings.Add(new Binding("Text", dtgvKho.DataSource, "IDMatHang", true, DataSourceUpdateMode.Never));
            txtTenMatHang.DataBindings.Add(new Binding("Text", dtgvKho.DataSource, "TenMatHang", true, DataSourceUpdateMode.Never));
            nmGiaNhap.DataBindings.Add(new Binding("text", dtgvKho.DataSource, "GiaNhap", true, DataSourceUpdateMode.Never));
            txtHSD.DataBindings.Add(new Binding("Text", dtgvKho.DataSource, "HanSuDung", true, DataSourceUpdateMode.Never));
            cbLoaiMH.DataBindings.Add(new Binding("Text", dtgvKho.DataSource, "IDLoaiMH", true, DataSourceUpdateMode.Never));
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

        void LoadQuanLiKho() 
        {
            string query = "SELECT  LMH.IDLoaiMH,LMH.TenLoaiMH, MH.IDMatHang, MH.TenMatHang , FORMAT(MH.GiaNhap , '0' ) AS GiaNhap , MH.HanSuDung\r\nFROM LOAI_MAT_HANG LMH JOIN MAT_HANG MH\r\nON LMH.IDLoaiMH = MH.IDLoaiMH;\r\n";

            dtgvKho.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        void AddAccount(string userName, string displayName, int type)
        {
          if (  AccountDAO.Instance.InsertAccount(userName, displayName, type))
          {
                MessageBox.Show("Thêm tài khoản thành công");
          }
          else
          {
                MessageBox.Show("Thêm tài khoản thất bại");
          }

          LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
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

        void ResetPass (string userName)
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

            accounts.Insert(0,item: new Account{ TenNguoiDung = "-- Chọn người dùng --" });

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

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;

            dtpStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);
        }

        void LoadBanAn()
        {
            dtgvBanAn.DataSource = TableDAO.Instance.GetTableList();
        }

        void AddTableFood() 
        {
            txtIDTableName.DataBindings.Clear();
            txtNameTable.DataBindings.Clear();
            cbKhuVuc.DataBindings.Clear();


            txtIDTableName.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameTable.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbKhuVuc.DataBindings.Add(new Binding("Text", dtgvBanAn.DataSource, "Zone", true, DataSourceUpdateMode.Never));

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

            if ( (int)Gia > 0 && idnhommon != null && iddvt != null)
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
            string userName = txtTenTk.Text;
            string displayName = txtTenHienThi.Text;
            int type = (int)nmLoaiTK.Value;

            EditAccount(userName, displayName, type);
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBanAn();
        }
    }
    #endregion
}