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
    public partial class ProfileMonAn : Form
    {
        public ProfileMonAn()
        {
            InitializeComponent();
        }

        private void btnNhomMon_Click(object sender, EventArgs e)
        {
            NhomMon f = new NhomMon();
            f.ShowDialog();
        }

        private void btnDVT_Click(object sender, EventArgs e)
        {
            DonViTinh f = new DonViTinh();
            f.ShowDialog();

        }
    }
}
