using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class WarehouseDAO
    {
        private static WarehouseDAO instance;

        public static WarehouseDAO Instance
        {
            get
            {
                if (instance == null) instance = new WarehouseDAO();
                return WarehouseDAO.instance;
            }
            private set { WarehouseDAO.instance = value; }
        }

        private WarehouseDAO() { }

        public DataTable GetListWarehouseByDate(string date1, string date2)
        {
            string query = "EXEC GetListKhoByDate @fisrtDate , @finalDate ";
            return DataProvider.Instance.ExcuteQuery(query, new object[] { date1, date2 });
        }
    }
}
