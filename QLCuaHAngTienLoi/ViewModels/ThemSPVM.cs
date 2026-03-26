using Microsoft.AspNetCore.Mvc.Rendering;
using QLCuaHAngTienLoi.data;

namespace QLCuaHAngTienLoi.ViewModels
{

    public class ThemSPVM
    {
        public SanPham SanPham { get; set; } = new SanPham();

        public List<SelectListItem> DanhMucList { get; set; } = new();

        public List<SelectListItem> NCCList { get; set; } = new();
    }
}