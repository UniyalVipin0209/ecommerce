namespace Beml.ECommerce.App.Models.VM
{
    public class DetailsVM 
    {
        public Product Product { get; set; }
        public Boolean ExistInCart { get; set; }

        public DetailsVM()
        {
            Product = new Product();
        }
    }
}
