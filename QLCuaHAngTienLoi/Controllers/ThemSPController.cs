using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;

public class ThemSPController : Controller
{
    private readonly QlcuaHangContext _context;

    public ThemSPController(QlcuaHangContext context)
    {
        _context = context;
    }

    private void LoadDropdown(ThemSPVM vm)
    {
        vm.DanhMucList = _context.DanhMucs
            .Select(x => new SelectListItem
            {
                Value = x.MaDanhMuc,
                Text = x.TenDanhMuc
            }).ToList();

        vm.NCCList = _context.NhaCungCaps
            .Select(x => new SelectListItem
            {
                Value = x.MaNcc,
                Text = x.TenCongTy
            }).ToList();
    }

    // GET
    public IActionResult Index()
    {
        var vm = new ThemSPVM();
        LoadDropdown(vm);
        return View(vm);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ThemSPVM vm)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdown(vm);
            return View(vm);
        }

        try
        {
            int count = _context.SanPhams.Count() + 1;

            vm.SanPham.MaSanPham = $"SP{count:D3}";
            vm.SanPham.Sku = $"SKU{count:D4}";
            vm.SanPham.GiaBan = vm.SanPham.GiaNhap * 1.3m;
            vm.SanPham.NgayThem = DateTime.Now;

            _context.SanPhams.Add(vm.SanPham);
            _context.SaveChanges();

            TempData["success"] = "✅ Thêm sản phẩm thành công!";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            LoadDropdown(vm);
            return View(vm);
        }
    }
}