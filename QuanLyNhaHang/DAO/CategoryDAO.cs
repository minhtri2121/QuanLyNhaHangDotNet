using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> GetCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select *\r\nfrom NHOM_MON";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category c = new Category(item);
                list.Add(c);
            }

            return list;
        }
        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string query = "select *\r\nfrom NHOM_MON where id = " + id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category c = new Category(item);
                return category;
            }
            return category;
        }
    }
}
