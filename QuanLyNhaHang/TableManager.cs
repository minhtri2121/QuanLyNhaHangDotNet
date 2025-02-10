using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;
using System.Drawing;

namespace QuanLyNhaHang
{
    public partial class FormTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount;}
            set
            {
                loginAccount = value;           
            }
        }

        public FormTableManager(Account acc)
        {
            InitializeComponent();

            LoadTable();

            this.LoginAccount = acc;
            ChangeAccount(loginAccount.Admin);
        }
        void ChangeAccount(int admin)
        {
            aDMINToolStripMenuItem.Enabled = admin == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + loginAccount.tenNguoiDung + ")";
            
        }

        #region Method
        void LoadTable()
        {
            List<Table> tablelist = TableDAO.Instance.LoadTableList();

            foreach (Table item in tablelist)
            {
                Button btn = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHieght,
                    Text = item.Name + Environment.NewLine + item.Status
                };
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Cornsilk;
                        break;
                    default:
                        btn.BackColor = Color.Crimson;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }
        #endregion

        #region Events
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.OnUpdateAccount += f_OnUpdateAccount;
            f.ShowDialog();
        }

        private void f_OnUpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.tenNguoiDung + ")"; 
        }

        private void aDMINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
            this.Show();
        }
        #endregion
    }
    
}