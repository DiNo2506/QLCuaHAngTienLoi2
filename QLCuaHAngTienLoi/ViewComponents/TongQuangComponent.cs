using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;
using System.Linq;

namespace QLCuaHAngTienLoi.ViewComponents
{
    // Ensure this component is found when calling Component.InvokeAsync("TongQuan")
    [ViewComponent(Name = "TongQuan")]
    public class TongQuanViewComponent : ViewComponent
    {
        private readonly QlcuaHangContext _db;
        public TongQuanViewComponent(QlcuaHangContext db) => _db = db;

        public IViewComponentResult Invoke()
        {
            // Low-stock list (threshold from MucCanhBao or default 10)
            var lowStock = _db.SanPhams
                .Include(sp => sp.MaNccNavigation)
                .Where(sp => (sp.TonKho ?? 0) < (sp.MucCanhBao ?? 10))
                .OrderBy(sp => sp.TonKho)
                .Select(sp => new LowStockItemVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? string.Empty,
                    TonKho = sp.TonKho ?? 0,
                    MucCanhBao = sp.MucCanhBao,
                   // NhaCungCap = sp.MaNccNavigation?.TenCongTy,
                    GiaBan = sp.GiaBan,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            // Recent products (most recent NgayThem)
            var recent = _db.SanPhams
                .Include(sp => sp.MaDanhMucNavigation)
                .OrderByDescending(sp => sp.NgayThem)
                .Take(3)
                .Select(sp => new ProductShortVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? string.Empty,
                    //Category = sp.MaDanhMucNavigation?.TenDanhMuc ?? string.Empty,
                    Price = sp.GiaBan ?? 0m,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            var vm = new TongQuanVM
            {
                TongSanPham = _db.SanPhams.Count(),
                TongGiaTriTonKho = _db.SanPhams.Any() ? _db.SanPhams.Sum(sp => (sp.GiaNhap ?? 0m) * (sp.TonKho ?? 0)) : 0m,
                SanPhamSapHet = lowStock.Count,
                TongDanhMuc = _db.DanhMucs.Count(),
                LowStockItems = lowStock,
                RecentProducts = recent
            };

            return View(vm);
        }
    }
}
