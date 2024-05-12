using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class StaffManageMainVM
    {
        public AccountAddVM? accountAddVM { get; set; }
        public List<Customer> customers { get; set; }
        public string? searchString { get; set; }

    }
}
