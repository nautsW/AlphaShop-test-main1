using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string? Password { get; set; }
    }
}
