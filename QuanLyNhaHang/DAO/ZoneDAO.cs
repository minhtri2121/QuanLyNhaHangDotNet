using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class ZoneDAO
    {
        private static ZoneDAO instance;

        public static ZoneDAO Instance
        {

            get { if (instance == null) instance = new ZoneDAO(); return ZoneDAO.instance; }
            private set { ZoneDAO.instance = value; }

        }
        private ZoneDAO() { }

        public List<Zone> GetTableListStatus()
        {
            List<Zone> tableListStatus = new List<Zone>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT IDKhuVuc, TenKhuVuc from KHU_VUC ");
            foreach (DataRow item in data.Rows)
            {
                Zone table = new Zone(item);
                tableListStatus.Add(table);
            }
            return tableListStatus;
        }
    }
}
