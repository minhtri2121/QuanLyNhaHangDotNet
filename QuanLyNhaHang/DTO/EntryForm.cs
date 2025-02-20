using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class EntryForm
    {
        public EntryForm(int maPhieuNhap, DateTime ngayNhap, string nguoiGiao, string nhaCungCap)
        {
            this.MaPhieuNhap = maPhieuNhap;
            this.NgayNhap = ngayNhap;
            this.NguoiGiao = nguoiGiao;
            this.NhaCungCap = nhaCungCap;
        }

        private int maPhieuNhap;
        private DateTime ngayNhap;
        private string nguoiGiao;
        private string nhaCungCap;

        public EntryForm(DataRow row)
        {
            this.MaPhieuNhap = (int)row["MaPhieuNhap"];
            this.NgayNhap = (DateTime)row["NgayNhap"];
            this.NguoiGiao = row["NguoiGiao"].ToString();
            this.NhaCungCap = row["NhaCungCap"].ToString();
        }

        public EntryForm() { }

        public int MaPhieuNhap
        {
            get { return maPhieuNhap; }
            set { maPhieuNhap = value; }
        }

        public DateTime NgayNhap
        {
            get { return ngayNhap; }
            set { ngayNhap = value; }
        }

        public string NguoiGiao
        {
            get { return nguoiGiao; }
            set { nguoiGiao = value; }
        }

        public string NhaCungCap
        {
            get { return nhaCungCap; }
            set { nhaCungCap = value; }
        }
    }
}

