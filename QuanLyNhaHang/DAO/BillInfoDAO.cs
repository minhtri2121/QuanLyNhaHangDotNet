using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get 
            { 
                if (instance == null) instance = new BillInfoDAO(); 
                return BillInfoDAO.instance; 
            }
            private set { BillInfoDAO.instance = value; }
        }
        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            string query = "exec GetListInfo @id ";
            DataTable data = DataProvider.Instance.ExcuteQuery(query,new object[] {id});
            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public bool InsertBillInfo(int idhoadon, int idmon, int soluong)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("EXEC InsertBillInfo @idhoadon , @idmon , @soluong ", new object[] { idhoadon, idmon, soluong });
            return result > 0;
        }
    }
}
