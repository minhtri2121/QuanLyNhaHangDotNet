using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class BanAn
    {
        public BanAn(int id, string name, string trangthai , string tenkhuvuc) { 
            this.Id = id;
            this.Name = name;
            this.Trangthai = trangthai;
            this.Tenkhuvuc = tenkhuvuc;
        }
        public BanAn(DataRow row)
        {
            this.Id = (int)row["IDBan"];
            this.Name = row["TenBan"].ToString();
            this.Trangthai = row["TrangThai"].ToString();
            this.tenkhuvuc = row["TenKhuVuc"].ToString();
        }

        private int id;
        private string name;
        private string tenkhuvuc;
        private string trangthai;

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
        public string Trangthai
        {
            get { return trangthai; }
            set { trangthai = value; }
        }

        public string Tenkhuvuc
        {
            get { return tenkhuvuc; }
            set { tenkhuvuc = value; }
        }
    }
}
