using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AlphaShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly HahaContext _context;
        private readonly AccountService _accountService;
        public OrderController(HahaContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;

        }
        public IActionResult Index()
        {
            decimal? total = 0;
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            foreach (CartDetail item in _context.CartDetails)
            {
                if(item.CartId == CtrId)
                {
                    total += item.Quantity * item.PrdPrice;
                }
            }
            Customer customer = _context.Customers.SingleOrDefault(p => p.CtrId ==  CtrId);
            CartModel cartModel = new CartModel()
            {
                cart = _context.Carts.SingleOrDefault(p => p.CartId == CtrId),
                cartDetail = _context.CartDetails.Where(p => p.CartId == CtrId).ToList(),
            };
            OrderModel orderModel = new OrderModel
            {
                destination = _context.Customers.SingleOrDefault(p => p.CtrId == CtrId).CtrAddress,
                note = "",
                Cart = cartModel,
                Total = total,
                Shipping = total + 45,
                customer = customer,
            };
            return View(orderModel);
        }
        public IActionResult Ordered()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ordered(OrderModel orderModel) 
        {
            Ord ord = new Ord
            {
                OrdId = _context.Ords.Count(),
                OrdDate = DateTime.Now,
                OrdDest = orderModel.destination,
                OrdStatus = false,
                CartId = orderModel.Cart.cart.CartId,
                OrdNote = orderModel.note,
                OrdPrice = orderModel.Total,
            };
            _context.Ords.Add(ord);
            _context.SaveChanges();
            foreach (var item in _context.CartDetails.ToList())
            {
                if (item.CartId == orderModel.Cart.cart.CartId)
                {

                    OrdDetail ordDetail = new OrdDetail
                    {
                        OrdId = ord.OrdId,
                        Note = item.Note,
                        Quantity = item.Quantity,
                        OptionSize = item.OptionSize,
                        OptionType = item.OptionType,
                        PrdId = item.PrdId,
                    };
                    _context.OrdDetails.Add(ordDetail);
                }
            }
            _context.SaveChanges();
            return View();
        }

    }
}
