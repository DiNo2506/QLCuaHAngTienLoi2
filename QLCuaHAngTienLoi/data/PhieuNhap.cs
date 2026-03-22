using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class PhieuNhap
{
    public string MaPhieuNhap { get; set; } = null!;

    public DateTime? NgayNhap { get; set; }

    public string? MaNcc { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
