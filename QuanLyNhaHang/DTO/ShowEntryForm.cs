using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class ShowEntryForm
    {
        public ShowEntryForm(int maPhieuNhap, DateTime ngayNhap, string nhaCungCap, string nguoiGiao,
                             string tenMatHang, string tenLoaiMatHang, int? soLuong, decimal? giaNhap, DateTime? hanSuDung)
        {
            this.MaPhieuNhap = maPhieuNhap;
            this.NgayNhap = ngayNhap;
            this.NhaCungCap = nhaCungCap;
            this.NguoiGiao = nguoiGiao;
            this.TenMatHang = tenMatHang;
            this.TenLoaiMatHang = tenLoaiMatHang;
            this.SoLuong = soLuong;
            this.GiaNhap = giaNhap;
            this.HanSuDung = hanSuDung;
        }
        private int maPhieuNhap;
        private DateTime ngayNhap;
        private string nhaCungCap;
        private string nguoiGiao;
        private string tenMatHang;
        private string tenLoaiMatHang;
        private int? soLuong;
        private decimal? giaNhap;
        private DateTime? hanSuDung;


        public ShowEntryForm(DataRow row)
        {
            this.MaPhieuNhap = (int)row["MaPhieuNhap"];
            this.NgayNhap = (DateTime)row["NgayNhap"];
            this.NguoiGiao = row["NguoiGiao"].ToString();
            this.NhaCungCap = row["NhaCungCap"].ToString();
            this.TenMatHang = row["TenMatHang"].ToString();
            this.TenLoaiMatHang = row["TenLoaiMH"].ToString();
            this.SoLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
            this.GiaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(row["GiaNhap"]) : 0m;
            this.HanSuDung = row["HanSuDung"] != DBNull.Value ? Convert.ToDateTime(row["HanSuDung"]) : (DateTime?)null;
        }

        public ShowEntryForm() { }

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

        public string NhaCungCap
        {
            get { return nhaCungCap; }
            set { nhaCungCap = value; }
        }

        public string NguoiGiao
        {
            get { return nguoiGiao; }
            set { nguoiGiao = value; }
        }

        public string TenMatHang
        {
            get { return tenMatHang; }
            set { tenMatHang = value; }
        }

        public string TenLoaiMatHang
        {
            get { return tenLoaiMatHang; }
            set { tenLoaiMatHang = value; }
        }

        public int? SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        public decimal? GiaNhap
        {
            get { return giaNhap; }
            set { giaNhap = value; }
        }

        public DateTime? HanSuDung
        {
            get { return hanSuDung; }
            set { hanSuDung = value; }
        }
    }
}
