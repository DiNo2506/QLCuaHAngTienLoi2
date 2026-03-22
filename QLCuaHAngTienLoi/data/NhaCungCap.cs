using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string? TenCongTy { get; set; }

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
