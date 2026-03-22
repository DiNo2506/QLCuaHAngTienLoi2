using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QLCuaHAngTienLoi.ViewModels;

namespace QLCuaHAngTienLoi.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly string connectionString;

        public BaoCaoController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("QLCuaHang");
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            var model = new BaoCaoVM
            {
                FromDate = fromDate ?? DateTime.Today.AddDays(-7),
                ToDate = toDate ?? DateTime.Today
            };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // KPI
                var cmd = new SqlCommand(@"
                    SELECT 
                        ISNULL(SUM(ct.ThanhTien),0),
                        COUNT(DISTINCT hd.MaHoaDon),
                        ISNULL(SUM(ct.SoLuong),0)
                    FROM HoaDon hd
                    JOIN ChiTietHoaDon ct ON hd.MaHoaDon = ct.MaHoaDon
                    WHERE hd.NgayLap BETWEEN @from AND @to", conn);

                cmd.Parameters.AddWithValue("@from", model.FromDate);
                cmd.Parameters.AddWithValue("@to", model.ToDate);

                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    model.DoanhThu = (decimal)rd[0];
                    model.SoDon = (int)rd[1];
                    model.SoLuong = (int)rd[2];
                }
                rd.Close();

                // Top sản phẩm
                var cmdTop = new SqlCommand(@"
                    SELECT TOP 5 sp.TenSanPham, SUM(ct.SoLuong)
                    FROM ChiTietHoaDon ct
                    JOIN SanPham sp ON sp.MaSanPham = ct.MaSanPham
                    JOIN HoaDon hd ON hd.MaHoaDon = ct.MaHoaDon
                    WHERE hd.NgayLap BETWEEN @from AND @to
                    GROUP BY sp.TenSanPham
                    ORDER BY SUM(ct.SoLuong) DESC", conn);

                cmdTop.Parameters.AddWithValue("@from", model.FromDate);
                cmdTop.Parameters.AddWithValue("@to", model.ToDate);

                var rdTop = cmdTop.ExecuteReader();
                while (rdTop.Read())
                {
                    model.TopSanPhams.Add(new TopSanPhamVM
                    {
                        TenSanPham = rdTop[0].ToString(),
                        SoLuongBan = (int)rdTop[1]
                    });
                }
                rdTop.Close();

                // Nhà cung cấp
                var cmdNCC = new SqlCommand(@"
                    SELECT TenCongTy, COUNT(MaSanPham), ISNULL(SUM(GiaNhap),0)
                    FROM NhaCungCap ncc
                    LEFT JOIN SanPham sp ON sp.MaNCC = ncc.MaNCC
                    GROUP BY TenCongTy", conn);

                var rdNCC = cmdNCC.ExecuteReader();
                while (rdNCC.Read())
                {
                    model.NhaCungCaps.Add(new NhaCungCapVM
                    {
                        TenCongTy = rdNCC[0].ToString(),
                        SoSanPham = (int)rdNCC[1],
                        GiaTri = (decimal)rdNCC[2]
                    });
                }
                rdNCC.Close();

                // Chart doanh thu theo ngày
                var cmdChart = new SqlCommand(@"
                    SELECT 
                        CONVERT(VARCHAR, hd.NgayLap, 23),
                        SUM(ct.ThanhTien)
                    FROM HoaDon hd
                    JOIN ChiTietHoaDon ct ON hd.MaHoaDon = ct.MaHoaDon
                    WHERE hd.NgayLap BETWEEN @from AND @to
                    GROUP BY CONVERT(VARCHAR, hd.NgayLap, 23)
                    ORDER BY 1", conn);

                cmdChart.Parameters.AddWithValue("@from", model.FromDate);
                cmdChart.Parameters.AddWithValue("@to", model.ToDate);

                var rdChart = cmdChart.ExecuteReader();
                while (rdChart.Read())
                {
                    model.ChartData.Add(new DoanhThuTheoNgayVM
                    {
                        Ngay = rdChart[0].ToString(),
                        TongTien = (decimal)rdChart[1]
                    });
                }
            }

            return View(model);
        }
    }
}