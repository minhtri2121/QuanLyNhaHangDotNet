using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static Account CurrentUser { get; private set; }

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public int IDNguoiDung
        {
            get; set;
        }

        public bool Login(string username, string password)
        {
            string query = "EXEC LoadLogin @username , @password ";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { username, password });
            if (result.Rows.Count > 0)
            {
                CurrentUser = new Account(result.Rows[0]);
                return true;
            }

            return false;
        }

        public bool UpdateAccount (string userName, string displayName , string pass, string newpass)
        {
           int result = DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @userName  , @displayName  , @password , @newPassword", new object[] {userName,displayName,pass,newpass});
            return result > 0;
        }

        public DataTable GetListAccount() 
        {
            return DataProvider.Instance.ExcuteQuery("SELECT IDNguoiDung, TenDangNhap, TenNguoiDung, CAST(Admin AS INT) AS Admin FROM Nguoi_Dung");
        }

        public List<Account> GetListAccounts()
        {
            List<Account> list = new List<Account>();

            string query = "SELECT * FROM NGUOI_DUNG";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Account account = new Account(row);
                list.Add(account);
            }

            return list;
        }

        public Account GetAccountByUserName(string username)
        {
            string query = "SELECT * FROM NGUOI_DUNG WHERE TenDangNhap = @username";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { username });

            if (data.Rows.Count > 0)
            {
                return new Account(data.Rows[0]);
            }
            return null;
        }


        internal Account GetAccountByAdmin(object admin)
        {
            throw new NotImplementedException();
        }

        public bool InsertAccount (string name , string displayName , int type )
        {
            string querry = string.Format("INSERT dbo.NGUOI_DUNG(TenDangNhap, TenNguoiDung, Admin) VALUES (N'{0}', N'{1}', {2})", name,displayName,type);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int type, int id)
        {
            try
            {
                string checkAdminQuery = "SELECT COUNT(*) FROM NGUOI_DUNG WHERE Admin = 1";
                int adminCount = (int)DataProvider.Instance.ExcuteNonScalar(checkAdminQuery);

                if (adminCount == 1)
                {
                    string checkCurrentAdminQuery = "SELECT COUNT(*) FROM NGUOI_DUNG WHERE Admin = 1 and IDNguoiDung = " + id;
                    int currentType = (int)DataProvider.Instance.ExcuteNonScalar(checkCurrentAdminQuery);

                    if (currentType == 1)
                    {
                        MessageBox.Show("Không thể chỉnh sửa vì đây là tài khoản Admin duy nhất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                string querry = string.Format("UPDATE dbo.NGUOI_DUNG SET  TenNguoiDung = N'{1}', Admin = N'{2}', TenDangNhap = N'{0}'  WHERE  IDNguoiDung = {3}", name, displayName, type, id);
                int result = DataProvider.Instance.ExcuteNonQuery(querry);
                return result > 0;
            }
            catch
            {
                MessageBox.Show("Lỗi, Đây là tài khoản ADMIN cuối cùng không được phép sửa!", "Cảnh Báo");
                return false;
            }

        }

        public bool DeleteAccount(string name)
        {
            string querry = string.Format("DELETE NGUOI_DUNG WHERE TenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string querry = string.Format("UPDATE NGUOI_DUNG SET MatKhau = N'0' WHERE TenDangNhap = N'{0}'", name);
            int result = DataProvider.Instance.ExcuteNonQuery(querry);

            return result > 0;
        }

        public int GetIdAccount(string name)
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteNonScalar("SELECT IDNguoiDung FROM NGUOI_DUNG WHERE TenDangNhap = N'" + name + "'");
            }
            catch
            {
                return 1;
            }
        }

        public string ResetPassword(string userInput, string displayName)
        {
            string query = "SELECT * FROM NGUOI_DUNG WHERE TenDangNhap = @userInput and TenNguoiDung = N'"+ displayName +"'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { userInput });

            if (data.Rows.Count > 0)
            {
                string newPassword = GenerateRandomPassword();
                string updateQuery = "UPDATE NGUOI_DUNG SET MatKhau = @newPassword WHERE TenDangNhap = @userInput and TenNguoiDung = N'"+displayName+"'";
                DataProvider.Instance.ExcuteNonQuery(updateQuery, new object[] { newPassword, userInput });

                return newPassword;
            }
            return null;
        }

        private string GenerateRandomPassword()
        {
            return "NewPass" + new Random().Next(1000, 9999).ToString();
        }
    }
}
