using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyNhaHang.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int ID)
        {
            List<Food> list = new List<Food>();
            string query = "select *\r\nfrom MON where IDNhomMon = '" + ID + "'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food f = new Food(item);
                list.Add(f);
            }

            return list;
        }

        public List<DVT> GetDVT()
        {
            List<DVT> list = new List<DVT>();
            string query = "SELECT * FROM DON_VI_TINH";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DVT d = new DVT(item);
                list.Add(d);
            }

            return list;
        }

        public bool InsertFood(string tukhoa, string tenmon, int? iddvt , int? idnhommon,int gia)
        {
            string query = "INSERT MON( TuKhoa, TenMon, IDDVT, IDNhomMon, Gia) VALUES(N'"+ tukhoa + "', N'" + tenmon + "', "+ iddvt + ", N'" + idnhommon + "', "+ gia + ")";
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(int idmon, string tukhoa, string tenmon, int? iddvt, int? idnhommon, int gia)
        {
            string query = "UPDATE dbo.MON SET TenMon = N'" + tenmon + "', TuKhoa = N'"+ tukhoa + "', IDDVT = "+ iddvt + ", IDNhomMon = " + idnhommon + ", Gia = "+ gia + " WHERE IDMon = '" + idmon+"'";
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }

        public List<SearchFood> SearchFood(string tenmon)
        {
            List<SearchFood> list = new List<SearchFood>();
            string query = "Select m.IDMon ,m.TuKhoa, m.TenMon, nm.TenNhomMon, dvt.TenDVT, FORMAT ( m.Gia ,'0') AS Gia from Mon m join nhom_mon nm  on m.IDNhomMon = nm.IDNhomMon join Don_vi_tinh dvt on dvt.IDDVT = m.IDDVT WHERE m.TenMon LIKE N'%" + tenmon + "%'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                SearchFood sf = new SearchFood(item);
                list.Add(sf);
            }

            return list;
        }


    }
}