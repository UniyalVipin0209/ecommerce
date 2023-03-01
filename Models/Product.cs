using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beml.ECommerce.App.Models
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string ShortDesc { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="Price should be greater than zero.")]
        public int Price { get; set; }

        #region "Category Relationship Implementation"
        [Display(Name="Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        #endregion

        #region "Application Relationship Implementation"
        [Display(Name = "Application Type")]
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType ApplicationType { get; set; }
        #endregion
    }
}
