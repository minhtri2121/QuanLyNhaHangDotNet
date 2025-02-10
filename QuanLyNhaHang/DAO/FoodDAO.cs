using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}