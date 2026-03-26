using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCuaHAngTienLoi.data;
using QLCuaHAngTienLoi.ViewModels;

namespace QLCuaHAngTienLoi.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly QlcuaHangContext _context;

        public HoaDonController(QlcuaHangContext context)
        {
            _context = context;
        }

        // 👉 Trang checkout
        public IActionResult Checkout()
        {
            return View();
        }

        // 👉 Tạo đơn hàng
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CheckoutVM model)
        {
            if (model == null || model.Items == null || model.Items.Count == 0)
                return BadRequest("Giỏ hàng rỗng");

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // 🔥 1. Tạo hóa đơn
                var hoaDon = new HoaDon
                {
                    MaHoaDon = Guid.NewGuid().ToString(),
                    NgayLap = DateTime.Now,
                    TongTien = 0
                };

                _context.HoaDons.Add(hoaDon);
                _context.SaveChanges();

                decimal tongTien = 0;

                // 🔥 2. Xử lý từng sản phẩm
                foreach (var item in model.Items)
                {
                    if (string.IsNullOrEmpty(item.Id) || item.Qty <= 0)
                        continue;

                    // 🔥 Lấy sản phẩm (TRACK để update được)
                    var sp = _context.SanPhams
                                     .FirstOrDefault(x => x.MaSanPham == item.Id);

                    if (sp == null)
                        throw new Exception($"Sản phẩm không tồn tại: {item.Id}");

                    // ❗ CHỐNG ÂM KHO
                    if (sp.TonKho < item.Qty)
                        throw new Exception($"Sản phẩm '{sp.TenSanPham}' không đủ hàng");

                    var donGia = sp.GiaBan  ;
                    var thanhTien = donGia * item.Qty;

                    // 🔥 TRỪ TỒN KHO
                    sp.TonKho -= item.Qty;

                    // 🔥 Tạo chi tiết hóa đơn
                    var ct = new ChiTietHoaDon
                    {
                        MaHoaDon = hoaDon.MaHoaDon,
                        MaSanPham = item.Id,
                        SoLuong = item.Qty,
                        DonGia = donGia,
                        ThanhTien = thanhTien
                    };

                    tongTien += thanhTien;

                    _context.ChiTietHoaDons.Add(ct);
                }

                if (tongTien == 0)
                    throw new Exception("Không có sản phẩm hợp lệ");

                // 🔥 3. Update tổng tiền
                hoaDon.TongTien = tongTien;

                _context.SaveChanges();
                transaction.Commit();

                return Ok(new
                {
                    success = true,
                    orderId = hoaDon.MaHoaDon
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        // 👉 Trang thành công
        public IActionResult Success(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return RedirectToAction("Checkout");

            ViewBag.OrderId = orderId;
            return View();
        }
    }
}