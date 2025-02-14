using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    internal class TableDAO
    {
        public static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance;  }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 80;
        public static int TableHieght = 80;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table> ();

            DataTable data = DataProvider.Instance.ExcuteQuery("EXEC LoadTable;");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public void SwitchTable(int id1, int id2, int idnguoidung)
        {
            string query = "USPSwitchTabe @idTable1 , @idTable2 , @idnguoidung ";
            DataProvider.Instance.ExcuteQuery(query, new object[] { id1, id2, idnguoidung });
        }
    }
}
