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

            LoadComboBoxTable(cbSwitchTable);
         
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
            listCategory.Insert(0, new Category { Id = -1, Name = "Danh mục món" });

            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }


        void LoadFoodByCategoryID(int categoryId)
        {
            List<Food> list = FoodDAO.Instance.GetFoodByCategoryID(categoryId);
            list.Insert(0, new Food { Id = -1, Name = "Chọn món ăn" });

            cbFood.DataSource = list;
            cbFood.DisplayMember = "Name";
            cbFood.ValueMember = "Id";
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

        private void ResizeListViewColumns()
        {
            // Tự động điều chỉnh chiều rộng cột dựa trên nội dung
            lsvBill.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); // Theo nội dung
            lsvBill.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize); // Theo tiêu đề
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

            ResizeListViewColumns();
        }

        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
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
            btnAddFood.Enabled = false;
            btnAddFood.Text = "Đang xử lý...";

            try
            {
                Table table = lsvBill.Tag as Table;
                if (table == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbFood.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idFood = (cbFood.SelectedItem as Food).Id;

                if (idFood == -1)
                {
                    MessageBox.Show("Vui lòng chọn món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int count = (int)nmFoodCount.Value;

                if (count <= 0)
                {
                    MessageBox.Show("Số lượng món ăn phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

                int idNguoiDung = AccountDAO.CurrentUser?.IdNguoiDung ?? -1;

                if (idBill == -1)
                {
                    if (!BillDAO.Instance.InsertBill(table.ID, idNguoiDung))
                    {
                        MessageBox.Show("Không thể tạo hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    idBill = BillDAO.Instance.GetMaxIdBill();
                }

                if (!BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count))
                {
                    MessageBox.Show("Không thể thêm món ăn vào hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ShowBill(table.ID);
                LoadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAddFood.Enabled = true;
                btnAddFood.Text = "Thêm món";
            }
        }


        private void btnCheck_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill == -1)
            {
                MessageBox.Show("Không tìm thấy hóa đơn chưa thanh toán cho bàn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int giamGia = (int)nmGiamGia.Value;

            if (string.IsNullOrWhiteSpace(txtTongTien.Text) || !double.TryParse(txtTongTien.Text.Split(',')[0], out double gia))
            {
                MessageBox.Show("Lỗi khi lấy tổng tiền! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double tongTien = gia - (gia / 100) * giamGia;

            DialogResult result = MessageBox.Show(
                string.Format("Bạn có muốn thanh toán cho bàn {0}?\nTổng tiền sau khi áp dụng giảm giá {1}% là: {2:N0} VNĐ",
                table.Name, giamGia, tongTien),
                "Xác nhận thanh toán", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                bool isCheckoutSuccess = BillDAO.Instance.CheckOut(idBill, giamGia, tongTien);
                if (isCheckoutSuccess)
                {
                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowBill(table.ID);
                    LoadTable();
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Xin hãy chọn bàn để chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            if (idBill == -1)
            {
                MessageBox.Show("Bàn chưa gọi món, không thể chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id1 = table.ID;
            int id2 = (cbSwitchTable.SelectedItem as Table)?.ID ?? -1;

            if (id2 == -1)
            {
                MessageBox.Show("Xin hãy chọn bàn để chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id1 == id2)
            {
                MessageBox.Show("Không thể chuyển bàn sang chính nó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idNguoiDung = AccountDAO.CurrentUser?.IdNguoiDung ?? -1;

            if (MessageBox.Show(string.Format("Bạn có muốn chuyển {0} sang {1} không?", table.Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableDAO.instance.SwitchTable(id1, id2, idNguoiDung);
                ShowBill(table.ID);
            }

            LoadTable();
        }

        private void btnMergeTable_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Xin hãy chọn bàn để gộp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            if (idBill == -1)
            {
                MessageBox.Show("Bàn chưa gọi món, không thể gộp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id1 = table.ID;
            int id2 = (cbSwitchTable.SelectedItem as Table)?.ID ?? -1;

            if (id2 == -1)
            {
                MessageBox.Show("Xin hãy chọn bàn để gộp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id1 == id2)
            {
                MessageBox.Show("Không thể gộp bàn sang chính nó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idNguoiDung = AccountDAO.CurrentUser?.IdNguoiDung ?? -1;

            if (MessageBox.Show(string.Format("Bạn có muốn gộp {0} và {1} không?", table.Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableDAO.instance.MergeTable(id1, id2, idNguoiDung);
                ShowBill(table.ID);
            }

            LoadTable();
        }

        #endregion

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}