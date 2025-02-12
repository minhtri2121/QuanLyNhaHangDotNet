using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class BillInfo
    {
        public BillInfo(int idhoadon, int idmon, int soluong, int dongia)
        {
            this.IdHoaDon = idhoadon;
            this.IdMon = idmon;
            this.SoLuong = soluong;
            this.DonGia = dongia;
        }

        public BillInfo(DataRow row)
        {
            this.IdHoaDon = (int)row["IDHoaDon"];
            this.IdMon = (int)row["IDMon"];
            this.SoLuong = (int)row["SoLuong"];
            this.DonGia = (int)row["DonGia"];
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
    }
}
