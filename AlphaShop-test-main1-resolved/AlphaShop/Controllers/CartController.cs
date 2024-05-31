using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;
using AlphaShop.Helpers;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AlphaShop.Controllers
{

    [Authorize]
    public class CartController : Controller
    {
        private readonly HahaContext db;

        private readonly AccountService _accountService;

        public CartController(AccountService accountService)
        {
            db = new HahaContext();
            _accountService = accountService;

        }
        public IActionResult Index()
        {
            int CartId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            CartModel cartModel = new CartModel()
            {
                cart = db.Carts.SingleOrDefault(p => p.CartId == CartId),
                cartDetail = db.CartDetails.Where(p => p.CartId == CartId).ToList(),
            };
            return View(cartModel);
        }

        public IActionResult AddToCart(int optsize, int opttype, int id)
        {

            CartDetail item = new CartDetail
            {
                CartId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value),
                PrdId = id,
                OptionSize = optsize,
                OptionType = opttype,
                Quantity = 1,
                PrdPrice = db.Products.SingleOrDefault(x => x.PrdId == id).PrdPrice,
                PrdName = db.Products.SingleOrDefault(x => x.PrdId == id).PrdName,
                PrdImage = db.Products.SingleOrDefault(x => x.PrdId == id).PrdImage,
            };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if (check == null)
            {
                //_accountService.Customer.Cart.CartDetails.Add(item);

                db.CartDetails.Add(item);
            }
            else
            {
                check.Quantity++;
                //foreach (CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                //{
                //    if (huukhoa.PrdId == id)
                //        huukhoa.Quantity = check.Quantity;
                //}
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
        public IActionResult DeleteCartItem(int cartid, int prdid, int optsize, int opttype)
        {
            Console.WriteLine($"DeleteCartItem called with parameters: cartid={cartid}, prdid={prdid}, optsize={optsize}, opttype={opttype}");

            var cartDetail = db.CartDetails.SingleOrDefault(x => x.CartId == cartid && x.PrdId == prdid && x.OptionSize == optsize && x.OptionType == opttype);
            if (cartDetail == null)
            {
                Console.WriteLine("Cart detail not found!");
                return NotFound();
            }
            db.Entry(cartDetail).State = EntityState.Modified;
            db.CartDetails.Remove(cartDetail);
            db.SaveChanges();



            Console.WriteLine("Cart detail deleted successfully!");

            return RedirectToAction("Index");
        }

        /*[HttpPost]
        public async Task<IActionResult> DeleteCartItem(int prdid, int optsize, int opttype)
        {
            int cartid = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            var cartDetail = db.CartDetails.SingleOrDefault(x => x.CartId == cartid && x.PrdId == prdid && x.OptionSize == optsize && x.OptionType == opttype);
            if (db.CartDetails == null)
            {
                return Problem("Entity set 'HahaContext.D'  is null.");
            }
            //var cartDetail = await db.CartDetails.FindAsync(cartid, prdid, optsize, opttype);
            if (cartDetail != null)
            {
                db.CartDetails.Remove(cartDetail);
                db.Entry(cartDetail).State = EntityState.Modified;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/

        public IActionResult UpdateCart(int optsize, int opttype, int id)
        {

            int cartid = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            CartDetail item = new CartDetail
            {
                CartId = cartid,
                PrdId = id,
                OptionSize = optsize,
                OptionType = opttype,
                Quantity = 1,
                PrdPrice = db.Products.FirstOrDefault(x => x.PrdId == id).PrdPrice,
                PrdName = db.Products.FirstOrDefault(x => x.PrdId == id).PrdName,
                PrdImage = db.Products.FirstOrDefault(x => x.PrdId == id).PrdImage,
            };
            var check = db.CartDetails.SingleOrDefault(x => x.CartId == item.CartId && x.PrdId == item.PrdId && x.OptionSize == item.OptionSize && x.OptionType == item.OptionType);
            if (check == null)
            {
                //_accountService.Customer.Cart.CartDetails.Add(item);

                db.CartDetails.Add(item);
            }
            else
            {
                check.Quantity++;
                //foreach (CartDetail huukhoa in _accountService.Customer.Cart.CartDetails)
                //{
                //    if (huukhoa.PrdId == id)
                //        huukhoa.Quantity = check.Quantity;
                //}
                db.Entry(check).State = EntityState.Modified;
            }

            db.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public IActionResult GetQuantity(int id)
        {
            int cartid = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            var check = db.CartDetails.Where(p => p.CartId == cartid).Count();
            return Json(check);
        }
    }

}
