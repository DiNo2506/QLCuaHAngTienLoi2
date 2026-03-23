using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;

namespace QLCuaHAngTienLoi.ViewComponents
{
    [ViewComponent(Name = "TongQuan")]
    public class TongQuanViewComponent : ViewComponent
    {
        private readonly QlcuaHangContext _db;

        public TongQuanViewComponent(QlcuaHangContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var lowStock = _db.SanPhams
                .Where(sp => (sp.TonKho ?? 0) < (sp.MucCanhBao ?? 10))
                .OrderBy(sp => sp.TonKho)
                .Select(sp => new LowStockItemVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? "",
                    TonKho = sp.TonKho ?? 0,
                    GiaBan = sp.GiaBan,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            var recent = _db.SanPhams
                .Include(sp => sp.MaDanhMucNavigation)
                .OrderByDescending(sp => sp.NgayThem ?? DateTime.MinValue)
                .Take(5)
                .Select(sp => new ProductShortVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? "",
                    Category = sp.MaDanhMucNavigation.TenDanhMuc,
                    Price = sp.GiaBan ?? 0,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            var vm = new TongQuanVM
            {
                TongSanPham = _db.SanPhams.Count(),
                TongGiaTriTonKho = _db.SanPhams.Sum(sp => (sp.GiaNhap ?? 0) * (sp.TonKho ?? 0)),
                SanPhamSapHet = lowStock.Count,
                TongDanhMuc = _db.DanhMucs.Count(),
                LowStockItems = lowStock,
                RecentProducts = recent
            };

            return View(vm);
        }
    }
}