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

        public void InsertBill(int idban, int idnguoidung)
        {
            DataProvider.Instance.ExcuteQuery("EXEC InsertBill @idban , @idnguoidung ", new object[] { idban, idnguoidung});
        }

        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteNonScalar("SELECT MAX(IDHoaDon) FROM HOA_DON");
            }
            catch
            {
                return 1;
            }
        }
    }
}
