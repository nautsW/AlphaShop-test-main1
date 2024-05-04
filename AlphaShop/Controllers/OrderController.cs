using AlphaShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly HahaContext _context;

        public OrderController(HahaContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}
