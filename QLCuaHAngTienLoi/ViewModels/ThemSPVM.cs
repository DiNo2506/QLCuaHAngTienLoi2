using Microsoft.AspNetCore.Mvc.Rendering;
using QLCuaHAngTienLoi.data;

namespace QLCuaHAngTienLoi.ViewModels
{
    public class ThemSPVM
    {
        public SanPham SanPham { get; set; }

        public List<SelectListItem>? DanhMucList { get; set; }

        public List<SelectListItem>? NCCList { get; set; }
    }
}