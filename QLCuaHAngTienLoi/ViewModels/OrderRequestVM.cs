namespace QLCuaHAngTienLoi.ViewModels
{
    public class OrderRequestVM
    {
        public int id { get; set; }     // MaSanPham
        public int qty { get; set; }
        public decimal price { get; set; }
    }
    public class OrderRequest
    {
        public List<OrderRequestVM> Items { get; set; }
    }
}
