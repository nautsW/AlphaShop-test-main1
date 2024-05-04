using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;

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
            foreach(CartDetail item in _context.CartDetails)
            {
                if(item.CartId == _accountService.Customer.CtrId)
                {
                    total += item.Quantity * item.PrdPrice;
                }
            }

            OrderModel orderModel = new OrderModel
            {
                destination = _accountService.Customer.CtrAddress,
                note = "",
                quantity = (int)_accountService.Customer.Cart.CartQuantity,
                Total = total,
                Shipping = total + 45

            };
            return View(orderModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(OrderModel orderModel)
        {
            Ord ord = new Ord
            {
                OrdId = _context.Ords.Count(),
                OrdDate = DateTime.Now,
                OrdDest = orderModel.destination,
                OrdStatus = false,
                CartId = _accountService.Customer.CtrId,
                OrdNote = orderModel.note,
                OrdPrice = orderModel.Total,
            };
            await _context.Ords.AddAsync(ord);
            foreach(var item in _context.CartDetails)
            {
                if (item.CartId == _accountService.Customer.CtrId)
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
            await _context.SaveChangesAsync();
            return Ordered();
        }
        public IActionResult Ordered() 
        {
            return View(); 
        
        }

    }
}
