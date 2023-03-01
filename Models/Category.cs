using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Beml.ECommerce.App.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Display Order for Category must be greater than zero")]
        public int DisplayOrder { get; set; }

        //Setting Default Value
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
