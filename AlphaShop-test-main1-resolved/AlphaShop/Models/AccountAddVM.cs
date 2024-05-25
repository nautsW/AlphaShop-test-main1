
using System.ComponentModel.DataAnnotations;

namespace AlphaShop.Models
{
    public class AccountAddVM
    {
        [Required]
        public int? idForUpdate { get; set; }
        [Required]
        public string? ctrLogUsername { get; set; }
        [Required]
        public string? ctrUsername { get; set; }
        [Required]
        public string? ctrPassword { get; set; }
        [Required]
        public bool? ctrGender { get; set; }
        [Required]
        public int? ctrStatus { get; set; }
        [Required]
        public string? ctrPhonenumber { get; set; }
        [Required]
        public string? ctrEmail { get; set; }
        [Required]
        public string? ctrAddress { get; set; }
        [Required]
        public int ctrAccess { get; set; }
    }
}
