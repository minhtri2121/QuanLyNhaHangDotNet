using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class BanAnDAO
    {
        private static BanAnDAO instance;

        public static BanAnDAO Instance
        {
            get { if (instance == null) instance = new BanAnDAO(); return BanAnDAO.instance; }
            private set { BanAnDAO.instance = value; }
        }
        private BanAnDAO() { }

        public List<BanAn> GetTableList()
        {
            List<BanAn> tableList = new List<BanAn>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT BAN.IDBan, BAN.TenBan, BAN.TrangThai, KHU_VUC.TenKhuVuc FROM BAN INNER JOIN KHU_VUC ON BAN.IDKhuVuc = KHU_VUC.IDKhuVuc ");
            foreach (DataRow item in data.Rows)
            {
                BanAn table = new BanAn(item);
                tableList.Add(table);
            }
            return tableList;
        }
    }
}
