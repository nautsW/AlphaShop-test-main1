namespace AlphaShop.Models
{
    public class AccountAddVM
    {
        public int? idForUpdate { get; set; }
        public string? ctrLogUsername { get; set; }
        public string? ctrUsername { get; set; }
        public string? ctrPassword { get; set; }
        public bool? ctrGender { get; set; }
        public int? ctrStatus { get; set; }
        public string? ctrPhonenumber { get; set; }
        public string? ctrEmail { get; set; }
        public string? ctrAddress { get; set; }
        public int ctrAccess { get; set; }
    }
}
