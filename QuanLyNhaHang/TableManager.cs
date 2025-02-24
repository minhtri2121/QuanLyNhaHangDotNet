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
using System.Drawing.Printing;
using System.IO;

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

        private PrintDocument printDocument = new PrintDocument();
        private string billContent = "";


        public FormTableManager(Account acc)
        {
            InitializeComponent();

            LoadTable();

            LoadCategory();

            this.LoginAccount = acc;

            ChangeAccount(loginAccount.Admin);

            LoadComboBoxTable(cbSwitchTable);

            lsvBill.SelectedIndexChanged += lsvBill_SelectedIndexChanged;

            InitializeListView();
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

        private void LoadFoodByCategoryID(int categoryId)
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

        private void InitializeListView()
        {
            InitializeListViewColumns();

            lsvBill.FullRowSelect = true;
            lsvBill.GridLines = true;
            lsvBill.MultiSelect = false;
        }

        private void InitializeListViewColumns()
        {
            lsvBill.Columns.Clear();

            int totalWidth = lsvBill.Width;

            lsvBill.Columns.Add("Tên món", (int)(totalWidth * 0.3));
            lsvBill.Columns.Add("Số lượng", (int)(totalWidth * 0.15));
            lsvBill.Columns.Add("Đơn giá", (int)(totalWidth * 0.19));
            lsvBill.Columns.Add("Thành tiền", (int)(totalWidth * 0.19));
            lsvBill.Columns.Add("Mã nhóm món", (int)(totalWidth * 0.17));

            lsvBill.View = View.Details;
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

                lsvItem.SubItems.Add(item.IdNhomMon.ToString());

                tongTien += item.ThanhTien;

                lsvBill.Items.Add(lsvItem);
            }

            txtTongTien.Text = tongTien.ToString("c");
        }

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvBill.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lsvBill.SelectedItems[0];

                string tenMon = selectedItem.SubItems[0].Text;
               
                if (selectedItem.SubItems.Count > 4)
                {
                    int categoryId = Convert.ToInt32(selectedItem.SubItems[4].Text);

                    UpdateCategoryComboBox(categoryId);

                    LoadFoodByCategoryID(categoryId);

                    SelectFoodInComboBox(tenMon, categoryId);
                }
                else
                {
                    ResetFoodComboBox();
                }
            }
            else
            {
                ResetFoodComboBox();
            }
        }

        private void SelectFoodInComboBox(string tenMon, int categoryId)
        {
            foreach (Category category in cbCategory.Items)
            {
                if (category.Id == categoryId)
                {
                    cbCategory.SelectedValue = categoryId;
                    break;
                }
            }
            foreach (Food food in cbFood.Items)
            {
                if (food.Name == tenMon)
                {
                    cbFood.SelectedItem = food;
                    break;
                }
            }
        }

        private void UpdateCategoryComboBox(int categoryId)
        {
            foreach (Category category in cbCategory.Items)
            {
                if (category.Id == categoryId)
                {
                    cbCategory.SelectedValue = categoryId;
                    break;
                }
            }
        }

        private void ResetFoodComboBox()
        {
            cbFood.DataSource = null;
            cbFood.Items.Clear();
            cbFood.Items.Add("Chọn món ăn");
            cbFood.SelectedIndex = 0;
        }

        void LoadComboBoxTable(ComboBox cb)
        {
            // Tạo đối tượng "Chọn bàn"
            Table selectTable = new Table() { ID = -1, Name = "Chọn bàn" };

            // Nạp danh sách bàn từ cơ sở dữ liệu
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            // Chèn "Chọn bàn" vào đầu danh sách
            tableList.Insert(0, selectTable);

            // Gán danh sách cho ComboBox
            cb.DataSource = tableList;
            cb.DisplayMember = "Name";

            // Thiết lập mục được chọn mặc định
            cb.SelectedIndex = 0;
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
            f.OnTableDeleted += F_OnTableDeleted;
            f.OnTableUpdated += F_OnTableUpdated;
            f.OnTableAdded += F_OnTableAdded;
            f.ShowDialog();
            this.Show();
        }

        private void F_OnTableDeleted(object sender, EventArgs e)
        {
            LoadTable();
        }


        private void F_OnTableUpdated(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void F_OnTableAdded(object sender, EventArgs e)
        {
            LoadTable();
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

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Xin hãy chọn bàn để hủy món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill == -1)
            {
                MessageBox.Show("Bàn này chưa gọi món hoặc đã thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idFood = (cbFood.SelectedItem as Food)?.Id ?? -1;
            if (idFood == -1)
            {
                MessageBox.Show("Xin chọn món để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BillInfoDAO.Instance.DeleteBillInfo(idBill, idFood);
                ShowBill(table.ID);
                LoadTable();
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
                    nmGiamGia.Value = 0;

                    // Chỉ tạo nội dung hóa đơn nếu thanh toán thành công
                    billContent = GenerateBillContent(table.Name, idBill, tongTien, giamGia);

                    // Đăng ký sự kiện in một lần
                    printDocument.PrintPage -= PrintDocument_PrintPage;
                    printDocument.PrintPage += PrintDocument_PrintPage;

                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = printDocument;
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GenerateBillContent(string tableName, int billID, double totalPrice, int discount)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("==============================================");
            sb.AppendLine("              HÓA ĐƠN THANH TOÁN              ");
            sb.AppendLine("==============================================");
            sb.AppendLine($" Bàn: {tableName}");
            sb.AppendLine($" Mã hóa đơn: {billID}");
            sb.AppendLine("==============================================");
            sb.AppendLine(string.Format("{0,-25} | {1,-3} | {2,10}", "Tên món", "SL", "Giá"));
            sb.AppendLine("----------------------------------------------");

            List<BillInfo> billDetails = BillInfoDAO.Instance.GetBillDetails(billID);

            foreach (var item in billDetails)
            {
                string tenMonFormatted = item.TenMon.Length > 22 ? item.TenMon.Substring(0, 19) + "..." : item.TenMon.PadRight(25);
                string soLuongFormatted = item.SoLuong.ToString().PadRight(3);
                string donGiaFormatted = (item.DonGia.ToString("N0") + " VNĐ").PadLeft(12);

                sb.AppendLine($"{tenMonFormatted} | {soLuongFormatted} | {donGiaFormatted}");
            }

            sb.AppendLine("----------------------------------------------");
            sb.AppendLine($" Giảm giá: {discount}%");
            sb.AppendLine($" Tổng tiền: {totalPrice:N0} VNĐ");
            sb.AppendLine("==============================================");
            sb.AppendLine("        CẢM ƠN QUÝ KHÁCH! HẸN GẶP LẠI!        ");
            sb.AppendLine("==============================================");

            return sb.ToString();
        }



        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 10, FontStyle.Bold);
            float yPos = e.MarginBounds.Top;
            float leftMargin = e.MarginBounds.Left;

            using (StringReader reader = new StringReader(billContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    e.Graphics.DrawString(line, font, Brushes.Black, leftMargin, yPos);
                    yPos += font.Height + 2;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(billContent))
            {
                MessageBox.Show("Không có hóa đơn nào để in!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PrintDocument printDoc = new PrintDocument();

            PaperSize paperSize = new PaperSize("Custom", 280, 600);
            printDoc.DefaultPageSettings.PaperSize = paperSize;

            printDoc.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
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

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        private void huỷMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnXoaMon_Click(this, new EventArgs());
        }

        private void chuyểnBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSwitchTable_Click(this, new EventArgs());
        }

        private void gộpBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnMergeTable_Click(this, new EventArgs());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheck_Click(this, new EventArgs());
        }

        #endregion

    }

}