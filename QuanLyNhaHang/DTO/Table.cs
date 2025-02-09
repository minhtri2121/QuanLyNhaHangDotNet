using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Table
    {
        public Table(int idtable, int idzone, string name, string status, string zone)
        {
            this.iD = idtable;
            this.idZone = idzone;
            this.Name = name;
            this.Status = status;
            this.Zone = zone;
        }
        
        private int iD;
        private int idZone;
        private string name;
        private string status;
        private string zone;

        public Table(DataRow row)
        {
            this.ID = (int) row["IDBan"];
            this.IdZone = (int)row["IDKhuVuc"];
            this.Name = row["TenBan"].ToString();
            this.Status = row["TrangThai"].ToString();
            this.Zone = row["TenKhuVuc"].ToString();
        }

        public int ID {
            get { return iD; }
            set { iD = value; }
        }

        public int IdZone
        {
            get { return idZone; }
            set { idZone = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Zone
        {
            get { return zone; }
            set { zone = value; }
        }
    }
}
