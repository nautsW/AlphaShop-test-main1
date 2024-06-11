using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            Customer customer = _context.Customers.SingleOrDefault(p => p.CtrId == CtrId);
            if(customer.CtrStatus == 2)
            {
                return RedirectToAction("OrderRestricted", "Order");
            }
            decimal? total = 0;
            foreach (CartDetail item in _context.CartDetails)
            {
                if (item.CartId == CtrId)
                {
                    total += item.Quantity * item.PrdPrice;
                }
            }
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

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(OrderModel orderModel)
        {
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            var lmao = _context.Carts.SingleOrDefault(x => x.CartId == CtrId);

            Ord ord = new Ord
            {
                OrdId = _context.Ords.Count(),
                OrdDate = DateTime.Now,
                OrdDest = orderModel.destination,
                OrdStatus = 0,
                CartId = CtrId,
                OrdNote = orderModel.note,
                OrdPrice = orderModel.Total,
            };

            foreach (var item in _context.CartDetails)
            {
                if (item.CartId == CtrId)
                {

                    OrdDetail ordDetail = new OrdDetail
                    {
                        OrdId = ord.OrdId,
                        Note = item.Note,
                        Quantity = item.Quantity,
                        OptionSize = item.OptionSize,
                        OptionType = item.OptionType,
                        PrdId = item.PrdId,
                        Prd = item.Prd,
                    };
                    ord.OrdDetails.Add(ordDetail);
                    _context.OrdDetails.Add(ordDetail);
                    _context.CartDetails.Remove(item);
                }
            }
            lmao.CartQuantity = 0;
            await _context.Ords.AddAsync(ord);
            await _context.SaveChangesAsync();
            return RedirectToAction("Ordered", "Order");
        }

        public IActionResult Ordered()
        {
            return View();
        }
        public IActionResult OrderRestricted()
        {
            return View();
        }
        /*public IActionResult Ordered(OrderModel orderModel) 
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
        }*/

    }
}
