using System.Collections.Generic;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class ProductListVM
    {
        public List<ProductItemVM> Items { get; set; } = new List<ProductItemVM>();

        // Pagination metadata
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
     

        public int TotalCount => Items?.Count ?? 0;
    }
}