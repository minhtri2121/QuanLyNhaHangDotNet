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

        public bool DeleteBillInfo(int idhoadon, int idmon)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("EXEC DeleteFoodinBillInfo @idhoadon , @idmon", new object[] { idhoadon, idmon });
            return result > 0;
        }

        public List<BillInfo> GetBillDetails(int billID)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            string query = "SELECT f.TenMon, bi.SoLuong, f.Gia FROM CTHOADON AS bi JOIN MON AS f ON bi.IDMon = f.IDMon WHERE bi.IDHoaDon = @billID";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { billID });

            foreach (DataRow row in data.Rows)
            {
                BillInfo billInfo = new BillInfo
                {
                    TenMon = row["TenMon"].ToString(),
                    SoLuong = (int)row["SoLuong"],
                    DonGia = Convert.ToInt32(row["Gia"])
                };

                listBillInfo.Add(billInfo);
            }

            return listBillInfo;
        }
    }
}
