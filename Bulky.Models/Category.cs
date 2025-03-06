using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category")]
        [MaxLength(30, ErrorMessage = "Name must in between 30 character")]
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Range(1, 100, ErrorMessage = "Display Order must be Ranges between(1-100)")]
        public int DisplayOrder { get; set; }
    }
}
