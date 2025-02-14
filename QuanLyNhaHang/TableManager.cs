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

        #region Method

        void ChangeAccount(int admin)
        {
            aDMINToolStripMenuItem.Enabled = admin == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + loginAccount.tenNguoiDung + ")";

        }

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
            flpTable.Controls.Clear();
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

            float tongTien = 0;

            List<QuanLyNhaHang.DTO.Menu> listBillInfo = MenuDAO.Instance.GetMuneByTableID(id);

            foreach (QuanLyNhaHang.DTO.Menu item in listBillInfo) 
            {
                ListViewItem lsvItem = new ListViewItem(item.TenMon.ToString());

                lsvItem.SubItems.Add(item.SoLuong.ToString());
                
                lsvItem.SubItems.Add(item.DonGia.ToString());

                lsvItem.SubItems.Add(item.ThanhTien.ToString());

                tongTien += item.ThanhTien;

                lsvBill.Items.Add(lsvItem);
            }

            txtTongTien.Text = tongTien.ToString("c");
        }

        #endregion

        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
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

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idFood = (cbFood.SelectedItem as Food).Id;

            int count = (int)nmFoodCount.Value;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            int idNguoiDung = AccountDAO.CurrentUser?.IdNguoiDung ?? -1;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID, idNguoiDung);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            ShowBill(table.ID);

            LoadTable();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            if(idBill != -1)
            {
                if (MessageBox.Show("Bạn có muốn thanh toán cho bàn " + table.Name, "Thông báo!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill);
                    ShowBill(table.ID);

                    LoadTable();
                }
            }
        }
        #endregion
    }
    
}