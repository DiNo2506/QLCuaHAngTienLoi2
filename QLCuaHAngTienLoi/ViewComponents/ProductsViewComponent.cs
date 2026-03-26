using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace QLCuaHAngTienLoi.ViewComponents
{
    // Make the component discoverable as "Products"
    [ViewComponent(Name = "Products")]
    public class ProductsViewComponent : ViewComponent
    {
        private readonly QlcuaHangContext _db;
        public ProductsViewComponent(QlcuaHangContext db) => _db = db;

        // page: 1-based, pageSize default 10
        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int pageSize = 10, string? search = null)
        {
            var q = _db.SanPhams
                .Include(p => p.MaDanhMucNavigation)
                .Include(p => p.MaNccNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                q = q.Where(p =>
                    (p.TenSanPham ?? "").Contains(search) ||
                    (p.Sku ?? "").Contains(search));
            }

            var totalItems = await q.CountAsync();

            q = q.OrderByDescending(p => p.NgayThem)
                 .Skip((Math.Max(page, 1) - 1) * Math.Max(pageSize, 1))
                 .Take(Math.Max(pageSize, 1));

            var list = await q.ToListAsync();



            var vm = new ProductListVM
            {
                Items = list.Select(p => new ProductItemVM
                {
                    MaSanPham = p.MaSanPham,
                    TenSanPham = p.TenSanPham ?? string.Empty,
                    Sku = p.Sku ?? string.Empty,
                    Category = p.MaDanhMucNavigation?.TenDanhMuc ?? string.Empty,
                    Company = p.MaNccNavigation?.TenCongTy ?? string.Empty,
                    Price = p.GiaBan  ,
                    Stock = p.TonKho  ,
                    NgayThem = p.NgayThem
                }).ToList(),
                Page = Math.Max(page, 1),
                PageSize = Math.Max(pageSize, 1),
                TotalItems = totalItems,
                TotalPages = Math.Max(1, (int)Math.Ceiling((double)totalItems / Math.Max(pageSize, 1)))
            };

            return View(vm);
        }
    }
}