using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(40)]
        [RegularExpression(@"^[a-zA-Z0-9\+]+$", ErrorMessage = "Log Username can only contain letters and numbers.")]
        public string? LogUsername { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(40)]
        [RegularExpression(@"^[\u0103\u1EA0-\u1EF9\p{L}\p{Nd}\w\s]+$", ErrorMessage = "Username can only contain letters, numbers, spaces, and hyphens.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9\+=-]+$", ErrorMessage = "Password must only contain letters, numbers")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match password. Please try again.")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [RegularExpression(@"^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.[a-zA-Z]{2,6})$", ErrorMessage = "Enter the correct email!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phonenumber must only contain numbers")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public DateTime? Birthday { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public bool? Gender { get; set; }
        public string? AccessId { get; set; } = "1";
        public string? Address { get; set; }
    }
}
