using AlphaShop.Data;
using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class DrinkAddVM
    {
        public int? idForUpdate { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public string? PrdName { get; set; }
        [Required]
        public decimal? PrdPrice { get; set; }
        [Required]
        public int? PrdStatus { get; set; }
        [Required]
        public IFormFile? PrdImage { get; set; }
        public string? PrdDesc { get; set; }
        public List<Category>? categories { get; set; }
    }
}
