using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class CategoryAddVM
    {
        [Required]
        public string? CategoryName { get; set; }
    }
}
