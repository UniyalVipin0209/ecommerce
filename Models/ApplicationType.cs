using System.ComponentModel.DataAnnotations;

namespace Beml.ECommerce.App.Models
{
    public class ApplicationType
    {
        [Key]
        public int ApplicationId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
