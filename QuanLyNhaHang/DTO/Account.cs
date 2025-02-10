using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Account
    {
        public Account(string TenDangNhap, string TenNguoiDung, int Admin, string MatKhau = null) 
        { 
            this.tenDangNhap = TenDangNhap;
            this.matKhau = MatKhau;
            this.tenNguoiDung = TenNguoiDung;   
            this.admin = Admin;
        }

        public Account(DataRow row)
        {
            this.tenDangNhap = row["TenDangNhap"].ToString();
            this.matKhau = row["MatKhau"].ToString();
            this.tenNguoiDung = row["TenNguoiDung"].ToString();
            this.admin = (int)row["Admin"];
        }

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


    }
}
