using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;

namespace QLCuaHAngTienLoi.Controllers
{
    public class ThemSPController : Controller
    {
        private readonly QlcuaHangContext _context;

        public ThemSPController(QlcuaHangContext context)
        {
            _context = context;
        }

        // ======================
        // GET: Form thêm sản phẩm
        // ======================
        public IActionResult Index()
        {
            var vm = new ThemSPVM
            {
                SanPham = new SanPham(),

                DanhMucList = _context.DanhMucs
                    .Select(d => new SelectListItem
                    {
                        Value = d.MaDanhMuc,
                        Text = d.TenDanhMuc
                    }).ToList(),

                NCCList = _context.NhaCungCaps
                    .Select(n => new SelectListItem
                    {
                        Value = n.MaNcc,
                        Text = n.TenCongTy
                    }).ToList()
            };

            return View(vm);
        }

        // ======================
        // POST: Lưu sản phẩm
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ThemSPVM vm)
        {
            if (ModelState.IsValid)
            {
                // ===== Tạo mã sản phẩm =====
                var lastProduct = _context.SanPhams
                    .OrderByDescending(p => p.MaSanPham)
                    .FirstOrDefault();

                int nextNumber = 1;

                if (lastProduct != null)
                {
                    string lastCode = lastProduct.MaSanPham.Substring(2);
                    nextNumber = int.Parse(lastCode) + 1;
                }

                vm.SanPham.MaSanPham = "SP" + nextNumber.ToString("D3");


                // ===== Tạo SKU =====
                var lastSKU = _context.SanPhams
                    .OrderByDescending(p => p.Sku)
                    .FirstOrDefault();

                int nextSKU = 1;

                if (lastSKU != null)
                {
                    string last = lastSKU.Sku.Substring(3);
                    nextSKU = int.Parse(last) + 1;
                }

                vm.SanPham.Sku = "SKU" + nextSKU.ToString("D4");


                // ===== Ngày thêm =====
                vm.SanPham.NgayThem = DateTime.Now;

                _context.SanPhams.Add(vm.SanPham);
                _context.SaveChanges();

                // quay về dashboard
                return RedirectToAction("Index", "Home");
            }

            vm.DanhMucList = _context.DanhMucs
                .Select(d => new SelectListItem
                {
                    Value = d.MaDanhMuc,
                    Text = d.TenDanhMuc
                }).ToList();

            vm.NCCList = _context.NhaCungCaps
                .Select(n => new SelectListItem
                {
                    Value = n.MaNcc,
                    Text = n.TenCongTy
                }).ToList();

            return View(vm);
        }
    }
}