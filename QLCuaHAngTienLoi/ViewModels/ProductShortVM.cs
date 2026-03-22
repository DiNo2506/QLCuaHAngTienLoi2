using System;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class ProductShortVM
    {
        public string MaSanPham { get; set; } = string.Empty;
        public string TenSanPham { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime? NgayThem { get; set; }
    }
}