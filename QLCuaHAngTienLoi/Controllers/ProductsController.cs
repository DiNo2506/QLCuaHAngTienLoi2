using Microsoft.AspNetCore.Mvc;

namespace QLCuaHAngTienLoi.Controllers
{
    public class ProductsController : Controller
    {
        // Redirect /Products to /SanPham (keeps one canonical route)
        public IActionResult Index(string? search, int page = 1, int pageSize = 10, string? sort = "all")
        {
            return RedirectToAction("Index", "SanPham", new { search, page, pageSize, sort });
        }
    }
}