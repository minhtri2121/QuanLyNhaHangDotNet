using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class BillInfo
    {
        public BillInfo(int idhoadon, int idmon, string tenmon ,int soluong, int dongia)
        {
            this.IdHoaDon = idhoadon;
            this.IdMon = idmon;
            this.TenMon = tenmon;
            this.SoLuong = soluong;
            this.DonGia = dongia;
        }

        public BillInfo(DataRow row)
        {
            this.IdHoaDon = (int)row["IDHoaDon"];
            this.IdMon = (int)row["IDMon"];
            this.TenMon = row["TenMon"].ToString();
            string rawData = row["SoLuong"]?.ToString().Trim();
            if (!int.TryParse(rawData, out int soLuong))
            {
                soLuong = 0; 
            }
            this.SoLuong = soLuong;
            this.SoLuong = (int)row["SoLuong"];
            this.DonGia = Convert.ToInt32(row["DonGia"]);

        }

        private int idHoaDon;
        private int idMon;
        private int soLuong;
        private int donGia;

        public int IdHoaDon
        {
            get { return idHoaDon; }
            set { idHoaDon = value; }
        }
        public int IdMon
        {
            get { return idMon; }
            set { idMon = value; }
        }
        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public int DonGia
        {
            get { return donGia; }
            set { donGia = value; }
        }

        public object TenMon { get; internal set; }
    }
}
