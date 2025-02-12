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
        private static BillInfoDAO intstance;

        public static BillInfoDAO Instance
        {
            get { return intstance; }
            private set { intstance = value; }
        }
        private BillInfoDAO() { }

        public  List<BillInfo> GetBillInfo()
        {
            List<BillInfo> listBillInfos = new List<BillInfo>();
            string query = "SELECT * FROM CTHOADON";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BillInfo bif = new BillInfo(item);
                listBillInfos.Add(bif);
            }
            return listBillInfos;
        }
    }
}
