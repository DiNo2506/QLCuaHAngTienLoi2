using System;
using System.Collections.Generic;
using System.Linq;

namespace QLCuaHAngTienLoi.Models
{
    public class CartItemDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }

    public class OrderDto
    {
        public string OrderId { get; set; } = "";
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public DateTime CreatedAt { get; set; }

        public decimal Total => Items?.Sum(i => i.Price * i.Qty) ?? 0m;
        public int TotalQty => Items?.Sum(i => i.Qty) ?? 0;
    }
}