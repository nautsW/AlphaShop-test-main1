using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop.Controllers
{
    public class StaffController : Controller
    {
        private readonly HahaContext _context;
        
        public StaffController(HahaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {   
            
            StaffIOrderManageVM vm = new StaffIOrderManageVM
            {
                orderlist = _context.Ords.Where(x => x.OrdStatus.HasValue && x.OrdStatus == 0).ToList(),
                NameList = new List<string>(),

            };
            foreach(var item in vm.orderlist)
            {
                vm.NameList.Add(_context.Customers.SingleOrDefault(x => x.CtrId == item.CartId).CtrUsername);
            }
           
            return View(vm);
        }
        
        public IActionResult OrderDetailManage(int id)
        {   OrderManage ordermanage = new OrderManage();
            Ord ord = _context.Ords.SingleOrDefault(p => p.OrdId == id);
            if (ord == null)
            {
                return NotFound();
            }
            else
            {
                List<OrdDetail> ordDetails = _context.OrdDetails.Where(p => p.OrdId == id).ToList();
                for (int i = 0; i < ordDetails.Count; i++)
                {
                    Product product = _context.Products.SingleOrDefault(p => p.PrdId == ordDetails.ElementAt(i).PrdId);
                    ordDetails.ElementAt(i).Prd = product;
                }
                ord.OrdDetails = ordDetails;
            }
            ordermanage.order = ord;
            ordermanage.customer = _context.Customers.SingleOrDefault(x => x.CtrId == ord.CartId);
            
            
            return View(ordermanage);
        }

        public IActionResult ManageOrder(int id)
        {
            _context.Ords.SingleOrDefault(x => x.OrdId == id).OrdStatus = 1;
            TempData["Alert"] = "<script>alert('Nhận hàng thành công!!')</script>";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CancelOrder(int id)
        {
            _context.Ords.SingleOrDefault(x => x.OrdId == id).OrdStatus = 2;
            TempData["Alert"] = "<script>alert('Hủy đơn hàng thành công ')</script>";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
