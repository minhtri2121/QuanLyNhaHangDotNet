using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            string query = "EXEC LoadLogin @username , @password ";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }

        public bool UpdateAccount (string userName, string displayName , string pass, string newpass)
        {
           int result = DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @userName  , @displayName  , @password , @newPassword", new object[] {userName,displayName,pass,newpass});
            return result > 0;
        }

        public Account GetAccountByUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from NGUOI_DUNG where TenDangNhap = '" + username + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        internal Account GetAccountByAdmin(object admin)
        {
            throw new NotImplementedException();
        }
    }
}
