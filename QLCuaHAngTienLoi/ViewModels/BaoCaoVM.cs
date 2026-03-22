using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class BaoCaoVM
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public decimal DoanhThu { get; set; }
        public int SoDon { get; set; }
        public int SoLuong { get; set; }

        public List<TopSanPhamVM> TopSanPhams { get; set; } = new();
        public List<NhaCungCapVM> NhaCungCaps { get; set; } = new();
        public List<DoanhThuTheoNgayVM> ChartData { get; set; } = new();
    }

    public class TopSanPhamVM
    {
        public string TenSanPham { get; set; }
        public int SoLuongBan { get; set; }
    }

    public class NhaCungCapVM
    {
        public string TenCongTy { get; set; }
        public int SoSanPham { get; set; }
        public decimal GiaTri { get; set; }
    }

    public class DoanhThuTheoNgayVM
    {
        public string Ngay { get; set; }
        public decimal TongTien { get; set; }
    }
}