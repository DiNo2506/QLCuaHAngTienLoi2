using System;
using System.Collections.Generic;

namespace QLCuaHAngTienLoi.data;

public partial class HoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public DateTime? NgayLap { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
}
