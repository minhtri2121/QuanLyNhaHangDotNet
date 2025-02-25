using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class FormForgotPassword : Form
    {
        public FormForgotPassword()
        {
            InitializeComponent();

            string displayName = txtDisplayNamee.Text?.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string displayNamee = txtDisplayNamee.Text;
            string displayName = displayNamee;
            string userInput = txtUserName.Text.ToString();

            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.");
                return;
            }          
            if (string.IsNullOrEmpty(displayName))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị.");
                return;
            }

            string newPassword = AccountDAO.Instance.ResetPassword(userInput, displayName);

            if (!string.IsNullOrEmpty(newPassword))
            {
                Clipboard.SetText(newPassword); 
                MessageBox.Show($"Mật khẩu mới của bạn là: {newPassword}\nVui lòng đổi mật khẩu sau khi đăng nhập!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản phù hợp!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
