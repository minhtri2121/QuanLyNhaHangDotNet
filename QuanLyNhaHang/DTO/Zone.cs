using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Zone
    {
        public Zone(int id, string name) 
        { 
            this.Id = id;
            this.Name = name;

        }
        public Zone(DataRow row) { 
            this.id = (int)row["IDKhuVuc"];
            this.name = row["TenKhuVuc"].ToString();
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
