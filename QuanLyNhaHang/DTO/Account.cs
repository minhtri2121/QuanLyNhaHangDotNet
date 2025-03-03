﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Account
    {
        public Account(int idnguoidung,string TenDangNhap, string TenNguoiDung, int Admin, string MatKhau = null) 
        { 
            this.IdNguoiDung = idnguoidung;
            this.tenDangNhap = TenDangNhap;
            this.matKhau = MatKhau;
            this.tenNguoiDung = TenNguoiDung;   
            this.admin = Admin;
        }

        public Account(DataRow row)
        {
            this.idNguoiDung = Convert.ToInt32(row["IDNguoiDung"]);
            this.tenDangNhap = row["TenDangNhap"].ToString();
            this.matKhau = row["MatKhau"].ToString();
            this.tenNguoiDung = row["TenNguoiDung"].ToString();

            if (row["Admin"] != DBNull.Value)
            {
                this.admin = Convert.ToInt32(row["Admin"]);
            }
            else
            {
                this.admin = 0; 
            }
        }

        public Account() { }

        public string TenDangNhap;
        public string tenDangNhap
        {
            get { return TenDangNhap; }
            set { TenDangNhap = value;}
        }


        public string TenNguoiDung;
        public string tenNguoiDung
        {
            get { return TenNguoiDung; }
            set { TenNguoiDung = value; }
        }


        public string MatKhau;
        public string matKhau
        {
            get { return MatKhau; }
            set { MatKhau = value; }
        }

        public int Admin;
        public int admin
        {
            get { return Admin; }
            set { Admin = value; }
        }

        public int idNguoiDung;
        public int IdNguoiDung
        {
            get { return idNguoiDung; }
            set { idNguoiDung = value; }
        }
    }
}
