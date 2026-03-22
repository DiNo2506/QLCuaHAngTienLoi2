using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class ChiTietPhieuNhap
{
    public int MaCtpn { get; set; }

    public string? MaPhieuNhap { get; set; }

    public string? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? GiaNhap { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual PhieuNhap? MaPhieuNhapNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
