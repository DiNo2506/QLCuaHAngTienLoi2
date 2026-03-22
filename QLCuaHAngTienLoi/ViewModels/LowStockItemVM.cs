using System;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class LowStockItemVM
    {
        public string MaSanPham { get; set; } = string.Empty;
        public string TenSanPham { get; set; } = string.Empty;
        public int TonKho { get; set; }
        public int? MucCanhBao { get; set; }
        public string? NhaCungCap { get; set; }
        public decimal? GiaBan { get; set; }
        public DateTime? NgayThem { get; set; }
    }
}