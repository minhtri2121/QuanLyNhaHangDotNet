﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Bill
    {
        public Bill(int idhoadon, DateTime? ngaylap, DateTime? giovao, DateTime? giora, string ca, int idnv, int idban, int giamgia = 0)
        {
            this.IdHoaDon = idhoadon;
            this.NgayLap = ngaylap;
            this.GioVao = giovao;
            this.GioThanhToan = giora;
            this.Ca = ca;
            this.IdNhanVien = idnv;
            this.IdBan = idban;
            this.GiamGia = giamgia;
        }

        public Bill(DataRow row)
        {
            this.IdHoaDon = (int)row["IDHoaDon"];
            this.NgayLap = (DateTime?)row["NgayLap"];

            var GioVaoCheck = row["GioVao"];

            if (GioVaoCheck.ToString() != "")
                this.GioVao = row["GioVao"] is DBNull ? null : row["GioVao"] as DateTime?;

            this.GioThanhToan = row["GioThanhToan"] as DateTime?;
            this.Ca = row["Ca"].ToString();
            this.IdNhanVien = (int)row["IDNguoiDung"];
            this.IdBan = (int)row["IDBan"];
            this.GiamGia = row["GiamGia"] != DBNull.Value ? Convert.ToInt32(row["GiamGia"]) : 0;

        }

        private int idHoaDon;
        private DateTime? ngayLap;
        private DateTime? gioVao;
        private DateTime? gioThanhToan;
        private string ca;
        private int idNhanVien;
        private int idBan;
        private int giamGia;

        public int IdHoaDon 
        {
            get {return idHoaDon; }
            set { idHoaDon = value; }
        }

        public DateTime? NgayLap
        {
            get { return ngayLap; }
            set { ngayLap = value; }
        }
        public DateTime? GioVao
        {
            get { return gioVao; }
            set { gioVao = value; }
        }
        public DateTime? GioThanhToan
        {
            get { return gioThanhToan; }
            set { gioThanhToan = value; }
        }
        public string Ca
        {
            get { return ca; }
            set { ca = value; }
        }
        public int IdNhanVien
        {
            get { return idNhanVien; }
            set { idNhanVien = value; }
        }
        public int IdBan
        {
            get { return idBan; }
            set { idBan = value; }
        }

        public int GiamGia
        {
            get { return giamGia; }
            set { giamGia = value; }
        }
    }
}