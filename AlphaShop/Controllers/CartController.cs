using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;
using AlphaShop.Helpers;
using Microsoft.EntityFrameworkCore;
namespace AlphaShop.Controllers
{
    public class CartController : Controller
    {
        private readonly HahaContext db;
        private readonly AccountService _accountService;

        public CartController(AccountService accountService)
        {
            db = new HahaContext();
            _accountService = accountService;
            
        }
        const string CART_KEY = "MYCART";
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id)
        {

            CartDetail item = new CartDetail
            {
                CartId = _accountService.Customer.CtrId,
                PrdId = id,
                OptionSize = 0,
                OptionType = 0,
                Quantity = 1,
            };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if (check == null)
            {
                _accountService.Customer.Cart.CartDetails.Add(item);
                
                db.CartDetails.Add(item);
            }
            else
            {
                item.Quantity++;
                foreach (CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                {
                    if (huukhoa.PrdId == id)
                        huukhoa.Quantity = item.Quantity;
                }
                db.Database.ExecuteSqlAsync($"Update dbo.CART_DETAIL SET QUANTITY = '{item.Quantity}' WHERE CART_ID = '{item.CartId}', PRD_ID = '{item.PrdId}', OPTION_SIZE = '{item.OptionSize}', OPTION_TYPE = '{item.OptionType}';");
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int id)
        {
           

                CartDetail item = new CartDetail
                {   
                    CartId = _accountService.Customer.CtrId,
                    PrdId = id,
                    OptionSize = 0,
                    OptionType = 0,
                    Quantity = 1,
                };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if(check == null)
            {
                _accountService.Customer.Cart.CartDetails.Add(item);
                db.CartDetails.Add(item);
            }
            else
            {
                int? a = item.Quantity++;
                foreach(CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                {
                    if (huukhoa.PrdId == id )
                    huukhoa.Quantity = a;
                }
                
                db.Database.ExecuteSqlRawAsync($"Update dbo.CART_DETAIL SET QUANTITY = '{a}' WHERE CART_ID = '{item.CartId}', PRD_ID = '{item.PrdId}', OPTION_SIZE = '{item.OptionSize}', OPTION_TYPE = '{item.OptionType}';");
            }
            db.SaveChanges();
            

            return RedirectToAction("Index", "Category");
        }
    }
}
