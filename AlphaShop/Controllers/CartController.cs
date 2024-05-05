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
        
        public IActionResult AddToCart(int optsize, int opttype, int id)
        {

            CartDetail item = new CartDetail
            {
                CartId = _accountService.Customer.CtrId,
                PrdId = id,
                OptionSize = optsize,
                OptionType = opttype,
                Quantity = 1,
                PrdPrice = db.Products.FirstOrDefault(x=>x.PrdId == id).PrdPrice,
                PrdName = db.Products.FirstOrDefault(x => x.PrdId == id).PrdName,
                PrdImage = db.Products.FirstOrDefault(x => x.PrdId == id).PrdImage,
            };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if (check == null)
            {
                _accountService.Customer.Cart.CartDetails.Add(item);

                db.CartDetails.Add(item);
            }
            else
            {
                check.Quantity++;
                foreach (CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                {
                    if (huukhoa.PrdId == id)
                        huukhoa.Quantity = check.Quantity;
                }
                db.Entry(check).State = EntityState.Modified;
            }

            db.SaveChangesAsync();


            //var item = new CartDetail
            //{
            //    CartId = _accountService.Customer.CtrId, 
            //    PrdId = productModel.product.PrdId,
            //    OptionSize = productModel.option_size,
            //    OptionType = productModel.option_type,
            //    Quantity = 1,
            //};

            
            //var check = db.CartDetails.SingleOrDefault(x => x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            //if (check == null)
            //{
                
            //    _accountService.Customer.Cart.CartDetails.Add(item);
            //    db.CartDetails.Add(item);
            //}
            //else
            //{
               
            //    item.Quantity++;
            //    db.CartDetails.Add(item);
            //}

            //db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCartItem(int cartid, int prdid, int optsize, int opttype)
        {
            var cartDetail = db.CartDetails.SingleOrDefault(x => x.CartId == cartid && x.PrdId == prdid && x.OptionSize == optsize && x.OptionType == opttype);
            if (db.CartDetails == null)
            {
                return Problem("Entity set 'AspLearningContext.Movie'  is null.");
            }
            //var cartDetail = await db.CartDetails.FindAsync(cartid, prdid, optsize, opttype);
            if (cartDetail != null)
            {
                db.CartDetails.Remove(cartDetail);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
      
        public IActionResult UpdateCart(int optsize, int opttype, int id)
        {


            CartDetail item = new CartDetail
            {
                CartId = _accountService.Customer.CtrId,
                PrdId = id,
                OptionSize =optsize,
                OptionType = opttype,
                Quantity = 1,
                PrdPrice = db.Products.FirstOrDefault(x => x.PrdId == id).PrdPrice,
                PrdName = db.Products.FirstOrDefault(x => x.PrdId == id).PrdName,
                PrdImage = db.Products.FirstOrDefault(x => x.PrdId == id).PrdImage,
            };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if (check == null)
            {
                _accountService.Customer.Cart.CartDetails.Add(item);

                db.CartDetails.Add(item);
            }
            else
            {
                check.Quantity++;
                foreach (CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                {
                    if (huukhoa.PrdId == id)
                        huukhoa.Quantity = check.Quantity;
                }
                db.Entry(check).State = EntityState.Modified;
            }

            db.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }
    }
}
