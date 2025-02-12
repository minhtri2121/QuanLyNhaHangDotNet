using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    public class BillDAO
    {
            private static BillDAO intstance;

            public static BillDAO Instance
            {
                get { return intstance; }
                private set { intstance = value; }
            }
            private BillDAO() { }

            public List<Bill> GetBillInfo()
            {
                List<Bill> listBillInfos = new List<Bill>();
                string query = "SELECT * FROM CTHOADON";
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                foreach (DataRow item in data.Rows)
                {
                    Bill bif = new Bill(item);
                    listBillInfos.Add(bif);
                }
                return listBillInfos;
            }
    }
}
