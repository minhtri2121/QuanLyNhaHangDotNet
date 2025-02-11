using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        BindingSource FoodList = new BindingSource();
        
        public fAdmin()
        {
            InitializeComponent();

            LoadAdmin();
        }

        // Code lại từ đây |

        #region methods

        private void LoadAdmin()
        {
            dtgvMonAn.DataSource = FoodList;

            LoadAccountList();

            LoadQuanLiKho();

            LoadListFood();

            LoadCategoryIntoComboBox(cbNhomMon);

            LoadDVTIntoComboBox(cbDVT);

            AddFoddBinding();
        }

        void LoadAccountList() //không dùng cái này
        {
            string query = "SELECT TenDangNhap, TenNguoiDung, MatKhau, Admin FROM NGUOI_DUNG";

            dtgvTaiKhoan.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadListFood() //không dùng cái này
        {
            string query = "SELECT TuKhoa, TenMon, TenNhomMon , TenDVT,FORMAT ( Gia ,'0') AS GiaTien\r\nFROM MON \r\nJOIN NHOM_MON ON MON.IDNhomMon = NHOM_MON.IDNhomMon\r\nJOIN DON_VI_TINH ON MON.IDDVT = DON_VI_TINH.IDDVT;";

            FoodList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadQuanLiKho() //không dùng cái này
        {
            string query = "SELECT  LMH.IDLoaiMH,LMH.TenLoaiMH, MH.IDMatHang, MH.TenMatHang , FORMAT(MH.GiaNhap , '0' ) AS GiaNhap , MH.HanSuDung\r\nFROM LOAI_MAT_HANG LMH JOIN MAT_HANG MH\r\nON LMH.IDLoaiMH = MH.IDLoaiMH;\r\n";

            dtgvKho.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void AddFoddBinding()
        {
            txtTenMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenMon", true, DataSourceUpdateMode.Never));
            txtTuKhoa.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TuKhoa", true, DataSourceUpdateMode.Never));
            cbNhomMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenNhomMon", true, DataSourceUpdateMode.Never));
            cbDVT.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenDVT", true, DataSourceUpdateMode.Never));
            nmGia.DataBindings.Add(new Binding("Value", dtgvMonAn.DataSource, "GiaTien", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetCategory();
            cb.DisplayMember = "Name";
        }
        void LoadDVTIntoComboBox(ComboBox cb)
        {
            cb.DataSource = FoodDAO.Instance.GetDVT();
            cb.DisplayMember = "Name";
        }

        #endregion

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
             int idnhommon = (cbNhomMon.SelectedItem as Category).Id;
            int iddvt = (cbDVT.SelectedItem as DVT).Id;
            float Gia = (float)nmGia.Value;

            if (FoodDAO.Instance.InsertFood(tukhoa, tenmon, iddvt, idnhommon, (int)Gia))
            {
                MessageBox.Show("Thêm món ăn thành công.");
                LoadListFood();
            }       
            else
            {
                MessageBox.Show("Có lỗi khi thêm món ăn.");
            }            
        }

    

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text;
            string tenmon = txtTenMon.Text;
            int idnhommon = (cbNhomMon.SelectedItem as Category).Id;
                int iddvt = (cbDVT.SelectedItem as DVT).Id;
            float Gia = (float)nmGia.Value;

            if (FoodDAO.Instance.InsertFood(tukhoa, tenmon, iddvt, idnhommon, (int)Gia))
            {
                MessageBox.Show("Sửa món ăn thành công.");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi Sửa món ăn.");
            }
        }
    }
}