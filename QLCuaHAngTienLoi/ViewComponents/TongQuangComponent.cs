using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;
using System.Linq;

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
                .Where(sp => (sp.TonKho ) < (sp.MucCanhBao ))
                .OrderBy(sp => sp.TonKho)
                .Select(sp => new LowStockItemVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? "",
                    TonKho = sp.TonKho ,
                    GiaBan = sp.GiaBan,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            var recent = _db.SanPhams
                .Include(sp => sp.MaDanhMucNavigation)
                .OrderByDescending(sp => sp.NgayThem )
                .Take(5)
                .Select(sp => new ProductShortVM
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham ?? "",
                    Category = sp.MaDanhMucNavigation.TenDanhMuc,
                    Price = sp.GiaBan ,
                    NgayThem = sp.NgayThem
                })
                .ToList();

            var vm = new TongQuanVM
            {
                TongSanPham = _db.SanPhams.Count(),
                TongGiaTriTonKho = _db.SanPhams.Sum(sp => (sp.GiaNhap ) * (sp.TonKho )),
                SanPhamSapHet = lowStock.Count,
                TongDanhMuc = _db.DanhMucs.Count(),
                LowStockItems = lowStock,
                RecentProducts = recent
            };

            return View(vm);
        }
    }
}