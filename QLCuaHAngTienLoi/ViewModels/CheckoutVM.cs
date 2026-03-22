using System.Collections.Generic;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class CartItemVM
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }

    public class CheckoutVM
    {
        public List<CartItemVM> Items { get; set; } = new();
    }
}