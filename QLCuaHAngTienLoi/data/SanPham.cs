using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLCuaHAngTienLoi.data;

public partial class SanPham
{
    [Key]
    public string MaSanPham { get; set; } = "";

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    public string TenSanPham { get; set; } = "";

    public string? Sku { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    public string MaDanhMuc { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
    public string MaNcc { get; set; } = "";

    [Required(ErrorMessage = "Nhập giá nhập")]
    [Range(1, double.MaxValue, ErrorMessage = "Giá phải > 0")]
    public decimal GiaNhap { get; set; }

    public decimal GiaBan { get; set; }

    public int TonKho { get; set; }

    public int MucCanhBao { get; set; }

    public DateTime NgayThem { get; set; } = DateTime.Now;
    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
