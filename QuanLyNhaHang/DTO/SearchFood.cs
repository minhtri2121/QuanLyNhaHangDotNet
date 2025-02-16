using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class SearchFood
    {
        public SearchFood(int idmon, string tukhoa, string tenmon, string tennhommon, string tendvt, int gia)
        {
            this.IdMon = idmon;
            this.TuKhoa = tukhoa;
            this.TenMon = tenmon;
            this.TenNhomMon = tennhommon;
            this.TenDVT = tendvt;
            this.Gia = gia;
        }

        private int idMon;
        private string tuKhoa;
        private string tenMon;
        private string tenNhomMon;
        private string tenDVT;
        private int gia;


        public int IdMon
        {
            get { return idMon; }
            set { idMon = value; }
        }

        public string TuKhoa
        {
            get { return tuKhoa; }
            set { tuKhoa = value; }
        }
        public string TenMon
        {
            get { return tenMon; }
            set { tenMon = value; }
        }
        public string TenNhomMon
        {
            get { return tenNhomMon; }
            set { tenNhomMon = value; }
        }
        public string TenDVT
        {
            get { return tenDVT; }
            set { tenDVT = value; }
        }
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }



        public SearchFood(DataRow row)
        {
            this.IdMon = row.Table.Columns.Contains("IDMon") && row["IDMon"] != DBNull.Value ? Convert.ToInt32(row["IDMon"]) : 0;
            this.TuKhoa = row.Table.Columns.Contains("TuKhoa") && row["TuKhoa"] != DBNull.Value ? row["TuKhoa"].ToString() : string.Empty;
            this.TenMon = row.Table.Columns.Contains("TenMon") && row["TenMon"] != DBNull.Value ? row["TenMon"].ToString() : string.Empty;
            this.TenNhomMon = row.Table.Columns.Contains("TenNhomMon") && row["TenNhomMon"] != DBNull.Value ? row["TenNhomMon"].ToString() : string.Empty;
            this.TenDVT = row.Table.Columns.Contains("TenDVT") && row["TenDVT"] != DBNull.Value ? row["TenDVT"].ToString() : string.Empty;
            this.Gia = row.Table.Columns.Contains("Gia") && row["Gia"] != DBNull.Value ? Convert.ToInt32(row["Gia"]) : 0;
        }

    }
}
