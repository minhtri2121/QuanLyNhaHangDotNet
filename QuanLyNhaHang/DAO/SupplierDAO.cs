using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class SupplierDAO
    {
        private static SupplierDAO instance;

        public static SupplierDAO Instance
        {

            get { if (instance == null) instance = new SupplierDAO(); return SupplierDAO.instance; }
            private set { SupplierDAO.instance = value; }

        }
        private SupplierDAO() { }

        public List<Supplier> GetSupplier()
        {
            List<Supplier> Supplier = new List<Supplier>();
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * from NHA_CUNG_CAP ");
            foreach (DataRow item in data.Rows)
            {
                Supplier s = new Supplier(item);
                Supplier.Add(s);
            }
            return Supplier;
        }
    }
}
