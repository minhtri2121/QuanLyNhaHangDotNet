﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class DVT
    {
        public DVT(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public DVT(DataRow row)
        {
            this.Id = (int)row["IDDVT"];
            this.Name = row["TenDVT"].ToString();
        }

        private int id;
        private string name;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
