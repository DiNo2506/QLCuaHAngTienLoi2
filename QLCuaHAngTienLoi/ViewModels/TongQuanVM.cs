using System.Collections.Generic;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class TongQuanVM
    {
        public int TongSanPham { get; set; }

        public decimal TongGiaTriTonKho { get; set; }

        public int SanPhamSapHet { get; set; }

        public int TongDanhMuc { get; set; }

        // Recently added products for dashboard
        public List<ProductShortVM> RecentProducts { get; set; } = new List<ProductShortVM>();

        // Low-stock items
        public List<LowStockItemVM> LowStockItems { get; set; } = new List<LowStockItemVM>();
    }
}
