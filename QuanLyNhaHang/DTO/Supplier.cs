using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Supplier
    {
        public Supplier(int idncc, string tenncc, string diachi, string dienthoai)
        {
            this.Idncc = idncc;
            this.Tenncc = tenncc;
            this.diaChi = diachi;
            this.dienThoai = dienthoai;
        }

       

        private int IDncc;
        private string TenNcc;
        private string DiaChi;
        private string DienThoai;

        public int Idncc
        {
            get { return Idncc; }
            set { Idncc = value; }
        }
        public string Tenncc
        {
            get { return TenNcc; }
            set { TenNcc = value; }
        }
        public string diaChi
        {
            get { return DiaChi; }
            set { DiaChi = value; }
        }
        public string dienThoai
        {
            get { return DienThoai; }
            set { DienThoai = value; }
        }
        public Supplier(DataRow row)
        {
            this.IDncc = (int)row["IDNCC"];
            this.TenNcc = row["TenNCC"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.DienThoai = row["DienThoai"].ToString();
        }
    }
}
