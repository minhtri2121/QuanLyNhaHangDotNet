using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value; 
                ChangeAccount(loginAccount);
            }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txtUserName.Text = LoginAccount.tenDangNhap;
            txtDisplay.Text = LoginAccount.tenNguoiDung;
        }
        
        void UpdateAccount()
        {
            string tenhienthi = txtDisplay.Text;
            string matkhau = txtPassWord.Text;
            string matkhaumoi = txtNewPass.Text;
            string reenter = txtReNewPass.Text;
            string tendangnhap = txtUserName.Text;
            if (!matkhaumoi.Equals(reenter))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else 
            {
                if(AccountDAO.Instance.UpdateAccount(tendangnhap,tenhienthi,matkhau,matkhaumoi))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    if (updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(tendangnhap)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu!");
                }
            }
        }

        private event EventHandler<AccountEvent>updateAccount;
        public event EventHandler<AccountEvent>OnUpdateAccount
        {
            add
            {
                updateAccount += value;
            }
            remove 
            { 
                updateAccount -= value; 
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private void cbShowPass1_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowPass1.Checked)
            {
                txtPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassWord.UseSystemPasswordChar = true;
            }
        }

        private void cbShowNewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowNewPass.Checked)
            {
                txtNewPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtNewPass.UseSystemPasswordChar = true;
            }
        }

        private void cbRePass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRePass.Checked)
            {
                txtReNewPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtReNewPass.UseSystemPasswordChar = true;
            }
        }
    }
    public class AccountEvent:EventArgs
    {
        private Account acc;

        public Account Acc { 
            get { return acc; } 
            set { acc = value; } 
        }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
