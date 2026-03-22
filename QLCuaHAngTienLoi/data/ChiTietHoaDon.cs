using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class ChiTietHoaDon
{
    public int MaCthd { get; set; }

    public string? MaHoaDon { get; set; }

    public string? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual HoaDon? MaHoaDonNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
