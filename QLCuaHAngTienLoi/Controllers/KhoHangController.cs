using Microsoft.AspNetCore.Mvc;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;


namespace QLCuaHAngTienLoi.Controllers
{
    public class KhoHangController : Controller
    {
        private readonly QlcuaHangContext _context;

        public KhoHangController(QlcuaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.SanPhams.ToList();

            var vm = new WarehouseViewModel
            {
                TotalProducts = products.Count,
                LowStockCount = products.Count(p => p.TonKho < p.MucCanhBao),
                Products = products,
                LowStockProducts = products
                    .Where(p => p.TonKho < p.MucCanhBao)
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ImportStock(string productId, int quantity, decimal importPrice, bool updatePrice)
        {

            var product = _context.SanPhams.FirstOrDefault(p => p.MaSanPham == productId);

            if (product == null)
            {
                return NotFound();
            }

            // cộng tồn kho
            product.TonKho += quantity;

            // tính giá bán mới
            decimal newSalePrice = importPrice * 1.3m;

            // nếu người dùng chọn cập nhật giá
            if (updatePrice)
            {
                product.GiaBan = newSalePrice;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

