using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    internal class ShowEntryFormDAO
    {
        public static ShowEntryFormDAO instance;
        public static ShowEntryFormDAO Instance
        {
            get { if (instance == null) instance = new ShowEntryFormDAO(); return ShowEntryFormDAO.instance; }
            private set { ShowEntryFormDAO.instance = value; }
        }
        private ShowEntryFormDAO() { }

        public List<DTO.ShowEntryForm> LoadShowEntryFormList()
        {
            List<DTO.ShowEntryForm> entryFormList = new List<DTO.ShowEntryForm>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT PN.IDPhieuNhap AS MaPhieuNhap, PN.NgayLapPN AS NgayNhap, PN.NguoiGiao AS NguoiGiao, NCC.TenNCC AS NhaCungCap, MH.TenMatHang, LMH.TenLoaiMH, CTPN.SoLuong, MH.GiaNhap, MH.HanSuDung FROM PHIEU_NHAP PN JOIN NHA_CUNG_CAP NCC ON NCC.IDNCC = PN.IDNCC LEFT JOIN CTPHIEUNHAP CTPN ON CTPN.IDPhieuNhap = PN.IDPhieuNhap LEFT JOIN MAT_HANG MH ON CTPN.IDMatHang = MH.IDMatHang LEFT JOIN LOAI_MAT_HANG LMH ON MH.IDLoaiMH = LMH.IDLoaiMH");
            foreach (DataRow item in data.Rows)
            {
                DTO.ShowEntryForm showEntryform = new DTO.ShowEntryForm(item);
                entryFormList.Add(showEntryform);
            }
            return entryFormList;
        }
    }
}