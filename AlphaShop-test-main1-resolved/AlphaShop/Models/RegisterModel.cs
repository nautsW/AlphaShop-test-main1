using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(40)]
        public string? LogUsername { get; set; }
        [Required]
        [StringLength(40)]
        public string? Username { get; set; }
        [Required]
        [StringLength(20)]
        public string? Password { get; set; }
        [Required]
        [StringLength(20)]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        [Required]
        public bool? Gender { get; set; }
        public string? AccessId { get; set; } = "1";
        public string? Address { get; set; }
    }
}
