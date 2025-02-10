﻿using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyNhaHang.DAO;

namespace QuanLyNhaHang
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();

            LoadAccountList();

            LoadQuanLiKho();

            LoadListFood();

            AddFoddBinding();
        }
        // Code lại từ đây |

        void LoadAccountList() //không dùng cái này
        {
            string query = "SELECT TenDangNhap, TenNguoiDung, MatKhau, Admin FROM NGUOI_DUNG";

            dtgvTaiKhoan.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadQuanLiKho() //không dùng cái này
        {
            string query = "SELECT  LMH.IDLoaiMH,LMH.TenLoaiMH, MH.IDMatHang, MH.TenMatHang , FORMAT(MH.GiaNhap , '0' ) AS GiaNhap , MH.HanSuDung\r\nFROM LOAI_MAT_HANG LMH JOIN MAT_HANG MH\r\nON LMH.IDLoaiMH = MH.IDLoaiMH;\r\n";

            dtgvKho.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadListFood() //không dùng cái này
        {
            string query = "SELECT TuKhoa, TenMon, TenNhomMon , TenDVT,FORMAT ( Gia ,'0') AS GiaTien\r\nFROM MON \r\nJOIN NHOM_MON ON MON.IDNhomMon = NHOM_MON.IDNhomMon\r\nJOIN DON_VI_TINH ON MON.IDDVT = DON_VI_TINH.IDDVT;";

            dtgvMonAn.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void AddFoddBinding() //không dùng cái này
        {
            txtTenMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenMon"));
            txtTuKhoa.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TuKhoa"));
            cbNhomMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenNhomMon"));
            cbDVT.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "TenDVT"));
            nmGia.DataBindings.Add(new Binding("Value", dtgvMonAn.DataSource, "GiaTien"));
        }
    }
}