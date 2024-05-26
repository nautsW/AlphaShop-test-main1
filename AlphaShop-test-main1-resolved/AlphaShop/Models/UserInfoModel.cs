using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class UserInfoVM
    {
        public ChangePasswordModel ChangePassword { get; set; }
        public ChangeImageModel imageModel { get; set; }
        public ChangePhoneNumberModel phoneNumberModel { get; set; }
        public ChangeEmailModel emailModel { get; set; }
        public Customer customer { get; set; }
        
        public string newAddress { get; set; }
    }
}
