using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhaHang.DTO;

namespace QuanLyNhaHang.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO() ; return  MenuDAO.instance; }
            private set { instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetMuneByTableID(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "EXEC GetMenuByTableID @id ";
            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                Menu m = new Menu(item);
                listMenu.Add(m);
            }
            return listMenu;
        }

    }
}
