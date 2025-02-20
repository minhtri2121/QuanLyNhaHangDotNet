using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    internal class EntryFormDAO
    {   
        public static EntryFormDAO instance;
        public static EntryFormDAO Instance 
        {
            get { if (instance == null) instance = new EntryFormDAO(); return EntryFormDAO.instance; }
            private set { EntryFormDAO.instance = value; }
        }

        private EntryFormDAO() { }

        public List<DTO.EntryForm> LoadEntryFormList()
        {
            List<DTO.EntryForm> entryFormList = new List<DTO.EntryForm>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT PHIEU_NHAP.IDPhieuNhap AS MaPhieuNhap, PHIEU_NHAP.NgayLapPN AS NgayNhap, PHIEU_NHAP.NguoiGiao as NguoiGiao, NHA_CUNG_CAP.TenNCC AS NhaCungCap FROM PHIEU_NHAP join NHA_CUNG_CAP on NHA_CUNG_CAP.IDNCC = PHIEU_NHAP.IDNCC");

            foreach (DataRow item in data.Rows)
            {
                DTO.EntryForm entryForm = new DTO.EntryForm(item);
                entryFormList.Add(entryForm);
            }

            return entryFormList;
        }


    }
}
