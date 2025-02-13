using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Menu
    {
        public Menu(string tenmon, int soluong, int dongia, int thanhtien)
        {
            this.TenMon = tenmon;
            this.SoLuong = soluong;
            this.DonGia = dongia;
            this.ThanhTien = thanhtien;
        }

        public Menu(DataRow row)
        {
            this.TenMon = row["TenMon"].ToString();
            this.SoLuong = (int)row["SoLuong"];
            this.DonGia = Convert.ToInt32(row["Gia"]);
            this.ThanhTien = Convert.ToInt32(row["DonGia"]);
        }

        private string tenMon;
        private int soLuong;
        private int donGia;
        private int thanhTien;

        public string TenMon
        {
            get { return tenMon; }
            set { tenMon = value; }
        }
        public int ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
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
