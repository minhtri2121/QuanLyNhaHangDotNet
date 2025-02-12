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

            LoadCategory();

            this.LoginAccount = acc;

            ChangeAccount(loginAccount.Admin);
        }
        void ChangeAccount(int admin)
        {
            aDMINToolStripMenuItem.Enabled = admin == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + loginAccount.tenNguoiDung + ")";
            
        }

        #region Method

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";

        }

        void LoadFoodByCategoryID(int id)
        {
            List<Food> list = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = list;
            cbFood.DisplayMember = "Name";
        }

        void LoadTable()
        {
            List<Table> tablelist = TableDAO.Instance.LoadTableList();

            foreach (Table item in tablelist)
            {
                Button btn = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHieght,
                };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;



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
        void ShowBill(int id) 
        {
            lsvBill.Items.Clear();

            List<BillInfo> listBillInfo = BillInfoDAO.Instance.GetListBillInfo(BillDAO.Instance.GetUncheckBillIDByTableID(id));

            foreach (BillInfo item in listBillInfo) 
            {
                ListViewItem lsvItem = new ListViewItem(item.IdHoaDon.ToString());

                if (item != null && item.TenMon != null)
                {
                    lsvItem.SubItems.Add(item.TenMon.ToString());
                }
                else
                {
                    lsvItem.SubItems.Add("Chưa có tên");
                }


                lsvItem.SubItems.Add(item.SoLuong.ToString());
                
                lsvItem.SubItems.Add(item.DonGia.ToString());

                lsvBill.Items.Add(lsvItem);
            }
        }

        #endregion

        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            ShowBill(tableID);
        }
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
            f.loginAccount = LoginAccount;
            f.ShowDialog();
            this.Show();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;

            id = selected.Id;

            LoadFoodByCategoryID(id);
        }
        #endregion
    }
    
}