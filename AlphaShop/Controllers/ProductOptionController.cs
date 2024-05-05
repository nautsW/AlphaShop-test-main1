//using AlphaShop.Data;
//using AlphaShop.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AlphaShop.Controllers
//{
//    public class ProductOptionController : Controller
//    {
//        private readonly HahaContext _context;
//        private readonly ProductModel _productModel;

//        public ProductOptionController(HahaContext context, ProductModel productModel)
//        {
//            _context = context;
//            _productModel = productModel;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Update(HahaContext context, ProductModel ProductModel)
//        {
//            var sizeForm = ProductModel.option_size;
//            var typeForm = ProductModel.option_type;

//            return RedirectToAction("Index", "Home");
//        }

//    }
//}
//}
