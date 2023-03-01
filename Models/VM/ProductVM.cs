using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beml.ECommerce.App.Models.VM
{
    public class ProductVM
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategoryListItem { get; set; }

        public IEnumerable<SelectListItem> ApplicationListItem { get; set; }
    }
}
