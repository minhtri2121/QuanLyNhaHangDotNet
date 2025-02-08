using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();

            LoadAccountList();

            LoadQuanLiKho();
        }

        void LoadAccountList()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionString);

            string query = "SELECT TenDangNhap, TenNguoiDung, MatKhau, Admin FROM NGUOI_DUNG";

            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(query, conn);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            adapter.Fill(data);

            conn.Close();

            dtgvTaiKhoan.DataSource = data;
        }

        void LoadQuanLiKho()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionString);

            string query = "SELECT *\r\nFROM LOAI_MAT_HANG JOIN MAT_HANG ON LOAI_MAT_HANG.IDLoaiMH = MAT_HANG.IDLoaiMH";

            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(query, conn);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            adapter.Fill(data);

            conn.Close();

            dtgvKho.DataSource = data;
        }
    }
}
