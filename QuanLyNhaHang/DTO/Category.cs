using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Category
    {
        public Category()
        {
            this.Id = id;
            this.Name = name;
        }

        public Category(DataRow row)
        {
            this.Id = (int)row["IDNhomMon"];
            this.Name = row["TenNhomMon"].ToString();
        }

        private int id;
        private string name;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
