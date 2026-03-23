using QLCuaHAngTienLoi.data;
using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class WarehouseViewModel
    {
        public int TotalProducts { get; set; }

        public int LowStockCount { get; set; }

        public List<SanPham> Products { get; set; }

        public List<SanPham> LowStockProducts { get; set; }
    }
}