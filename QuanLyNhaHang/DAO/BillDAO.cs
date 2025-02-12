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
            private static BillDAO instance;

            public static BillDAO Instance
            {
                get 
                {
                    if (instance == null) instance = new BillDAO();
                    return instance; 
                }
                private set { BillDAO.instance = value; }
            }
            private BillDAO() { }

            public int GetUncheckBillIDByTableID(int id)
            {
                DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM HOA_DON WHERE IDBan = " + id + " AND TrangThai = N'Chưa thanh toán'");
                if (data.Rows.Count > 0)
                {
                    Bill bill = new Bill(data.Rows[0]);
                    return bill.IdHoaDon;
                }
                return -1;
            }

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
