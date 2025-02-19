using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> GetCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select *\r\nfrom NHOM_MON";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category c = new Category(item);
                list.Add(c);
            }

            return list;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string query = "select *\r\nfrom NHOM_MON where id = " + id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category c = new Category(item);
                return category;
            }
            return category;
        }

        public List<Items> GetItems()
        {
            List<Items> list = new List<Items>();
            string query = "select *\r\nfrom LOAI_MAT_HANG";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Items i = new Items(item);
                list.Add(i);
            }
            return list;
        }

        public bool InsertCategory(string tenmathang, int gianhap, string hansudung, int? idloaimh)
        {
            string query = "INSERT MAT_HANG( TenMatHang, GiaNhap, HanSuDung, IDLoaiMH ) VALUES ( N'" + tenmathang + "', " + gianhap + ", '" + hansudung + "', " + idloaimh + ")";
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateCategory(int idmathang, string tenmathang, int gianhap, string hansudung, int? idloaimh)
        {
            string query = "EditMatHang @idMatHang , @tenMatHang , @giaNhap  , @hanSD , @idLoaiMH  ";
            int result = DataProvider.Instance.ExcuteNonQuery(query, new object[] { idmathang, tenmathang, gianhap, hansudung, idloaimh });

            return result > 0;
        }

        public List<SearchCategory> SearchCategory(string tenmathang)
        {
            List<SearchCategory> list = new List<SearchCategory>();
            string query = "select MH.IDMatHang, MH.TenMatHang, FORMAT ( MH.GiaNhap ,'0') AS GiaNhap, MH.HanSuDung, MH.IDLoaiMH from MAT_HANG MH JOIN LOAI_MAT_HANG LMH on MH.IDMatHang = LMH.IDLoaiMH WHERE MH.TenMatHang LIKE N'%" + tenmathang + "%'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                SearchCategory sc = new SearchCategory(item);
                list.Add(sc);
            }
            //
            return list;
        }
    }
}
