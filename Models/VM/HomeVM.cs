using Beml.ECommerce.App.Models;

namespace Beml.ECommerce.App.Models.VM
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<ApplicationType>? ApplicationTypes { get; set; }
    }
}
