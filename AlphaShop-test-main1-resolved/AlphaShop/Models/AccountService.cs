using AlphaShop.Data;
using SQLitePCL;

namespace AlphaShop.Models
{
    public class AccountService
    {
        private readonly HahaContext _context = new HahaContext();
        public bool IsLoggedIn { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public string SDT { get; set; }
        //public string Mail { get; set; }
        //public string Img_url { get; set; }

        public Customer Customer { get; set; }


    }
}
