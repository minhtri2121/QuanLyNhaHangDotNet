using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaHang
{
    public partial class ProfilePN : Form
    {
        public ProfilePN()
        {
            InitializeComponent();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            NhaCungCap ncc = new NhaCungCap();
            ncc.ShowDialog();
        }

        private void btnLoaiMH_Click(object sender, EventArgs e)
        {

        }
    }
}
