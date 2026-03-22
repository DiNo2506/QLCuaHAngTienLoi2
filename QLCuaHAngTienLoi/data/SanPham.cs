using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class SanPham
{
    public string MaSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public string? Sku { get; set; }

    public string? MaDanhMuc { get; set; }

    public string? MaNcc { get; set; }

    public decimal? GiaNhap { get; set; }

    public decimal? GiaBan { get; set; }

    public int? TonKho { get; set; }

    public int? MucCanhBao { get; set; }

    public DateTime? NgayThem { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
