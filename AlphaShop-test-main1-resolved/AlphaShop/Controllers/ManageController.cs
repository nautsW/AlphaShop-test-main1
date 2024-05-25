using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace AlphaShop.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly HahaContext _context;
        private readonly IWebHostEnvironment wwwroot;
        public ManageController(IWebHostEnvironment webHostEnvironment)
        {
            _context = new HahaContext();
            wwwroot = webHostEnvironment;
        }
        public IActionResult Index()
        {
            TempData["Check"] = "Index";
            return View();

        }
        public IActionResult DrinkManage()
        {
            DrinkAddVM vm = new DrinkAddVM
            {
                categories = _context.Categories.ToList(),
            };
            DrinkManageMainVM drinkManageMainVM = new DrinkManageMainVM
            {
                addModel = vm,
                addCategory = new CategoryAddVM(),
                products = _context.Products.ToList(),

            };
            TempData["Check"] = "DrinkManage";
            return View(drinkManageMainVM);
        }
        [HttpPost]
        public IActionResult AddDrink(DrinkAddVM VM)
        {

            DrinkAddVM vM = VM;
            var check = _context.Products.SingleOrDefault(p => p.PrdName == VM.PrdName);
            if (check == null)
            {
                string filename = ImageFileUpload(vM);
                Product product = new Product
                {
                    PrdId = _context.Products.Count() + 1,
                    CgrId = vM.CategoryID,
                    PrdName = vM.PrdName,
                    PrdPrice = vM.PrdPrice,
                    PrdDesc = vM.PrdDesc,
                    PrdStatus = vM.PrdStatus,
                    PrdImage = (vM.PrdImage == null) ? "assets/img/drinklist/png-clipart-fizzy-drinks-logo-cocktail-glass-cocktail-glass-logo.png" : "assets/img/drinklist/" + "_" + filename,
                    PrdVisible = true
                };
                _context.Products.Add(product);
            }
            else
            {
                string filename = ImageFileUpload(vM);
                check.CgrId = vM.CategoryID;
                check.PrdName = vM.PrdName;
                check.PrdPrice = vM.PrdPrice;
                check.PrdStatus = vM.PrdStatus;
                check.PrdDesc = vM.PrdDesc;
                check.PrdImage = (vM.PrdImage == null) ? "assets/img/drinklist/png-clipart-fizzy-drinks-logo-cocktail-glass-cocktail-glass-logo.png" : "assets/img/drinklist/" + "_" + filename;
                check.PrdVisible = true;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
            }
            _context.SaveChanges();
            TempData["msg"] = "<script>alert('Change succesfully');</script>";
            return RedirectToAction("DrinkManage");
        }
        private string ImageFileUpload(DrinkAddVM vM)
        {
            string filename = null;
            if (vM.PrdImage != null && vM.PrdImage.Length > 0)
            {

                string uploadDir = Path.Combine(wwwroot.WebRootPath, "assets", "img", "drinklist");
                filename = Guid.NewGuid().ToString() + "-" + vM.PrdImage.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vM.PrdImage.CopyTo(fileStream);
                }
            }
            return filename;
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryAddVM VM)
        {
            var check = _context.Categories.SingleOrDefault(p => p.CgrName == VM.CategoryName);
            if (check == null)
            {
                Category category = new Category
                {
                    CgrId = _context.Categories.Count() + 1,
                    CgrName = VM.CategoryName,
                };
                _context.Categories.Add(category);
            }
            _context.SaveChanges();
            TempData["msg"] = "<script>alert('Change succesfully');</script>";
            return RedirectToAction("DrinkManage");

        }




        [HttpPost]
        public IActionResult Update(DrinkAddVM vm)
        {

            var check = _context.Products.FirstOrDefault(p => p.PrdId == vm.idForUpdate);
            if (check == null)
            {
                return NotFound();
            }
            else
            {
                string filename = ImageFileUpload(vm);
                check.PrdName = vm.PrdName;
                check.PrdPrice = vm.PrdPrice;
                check.PrdStatus = vm.PrdStatus;
                check.PrdDesc = vm.PrdDesc;
                if (vm.PrdImage != null) check.PrdImage = "assets/img/drinklist/" + "_" + filename;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["msg"] = "<script>alert('Change succesfully');</script>";

            }
            return RedirectToAction("DrinkManage");
        }
        [HttpPost]
        public IActionResult Delete(DrinkAddVM vm)
        {
            var check = _context.Products.SingleOrDefault(p => p.PrdId == vm.idForUpdate);
            if (check == null)
            {
                return NotFound();

            }
            else
            {
                //_context.Entry(check).State = EntityState.Modified;
                //_context.Remove(check);
                //_context.SaveChanges();
                check.PrdVisible = false;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["msg"] = "<script>alert('Change succesfully');</script>";
            }
            return RedirectToAction("DrinkManage");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpGet]
        //public JsonResult GetDrinkDetails(int drinkId)
        //{
        //    // Replace with your data access logic to fetch drink details based on drinkId
        //    var selectedDrink = _context.Products.SingleOrDefault(p => p.PrdId == drinkId);
        //    if (selectedDrink == null)
        //    {
        //        return Json(new { error = "Drink not found" }); // Handle drink not found scenario
        //    }

        //    return Json(selectedDrink); // Return drink details as JSON
        //}
        //[HttpPost]
        //public IActionResult RenderDrinkInfoPopup(Product drink)
        //{
        //    return PartialView("_DrinkInfo", drink); // Render partial view with drink data
        //}


        public IActionResult CustomerManage()
        {
            List<Customer> temp_list = _context.Customers.Where(p => p.CtrAccess == 1).ToList();
            foreach (Customer customer in temp_list)
            {
                List<Ord> ords = _context.Ords.Where(p => p.CartId == customer.CtrId).ToList();
                customer.Ords = ords;
                foreach (Ord ord in customer.Ords)
                {
                    List<OrdDetail> ordDetails = _context.OrdDetails.Where(p => p.OrdId == ord.OrdId).ToList();
                    ord.OrdDetails = ordDetails;
                }
            }
            AccountAddVM accountAddVM = new AccountAddVM
            {

            };
            StaffManageMainVM mainVM = new StaffManageMainVM
            {
                customers = temp_list,
                accountAddVM = accountAddVM,

            };
            TempData["Check"] = "CustomerManage";
            return View(mainVM);
        }
        public IActionResult StaffManage()
        {
            List<Customer> temp_list = _context.Customers.Where(p => p.CtrAccess == 2).ToList();
            foreach (Customer customer in temp_list)
            {
                List<Ord> ords = _context.Ords.Where(p => p.StaffId == customer.CtrId).ToList();
                customer.Ords = ords;
                foreach (Ord ord in customer.Ords)
                {
                    List<OrdDetail> ordDetails = _context.OrdDetails.Where(p => p.OrdId == ord.OrdId).ToList();
                    ord.OrdDetails = ordDetails;
                }
            }
            AccountAddVM accountAddVM = new AccountAddVM
            {

            };
            StaffManageMainVM mainVM = new StaffManageMainVM
            {
                customers = temp_list,
                accountAddVM = accountAddVM,

            };
            TempData["Check"] = "StaffManage";
            return View(mainVM);
        }
        [HttpPost]
        public IActionResult AccountAdd(AccountAddVM accountAddVM)
        {
            var check = _context.Customers.SingleOrDefault(p => p.CtrLogusername == accountAddVM.ctrLogUsername);
            if (check != null)
            {
                TempData["msg"] = "<script>alert('Error: username is duplicated');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                Customer customer = new Customer
                {
                    CtrId = _context.Customers.Count(),
                    CtrLogusername = accountAddVM.ctrLogUsername,
                    CtrUsername = accountAddVM.ctrUsername,
                    CtrPassword = accountAddVM.ctrPassword,
                    CtrPhonenumber = accountAddVM.ctrPhonenumber,
                    CtrGender = accountAddVM.ctrGender,
                    CtrAccess = _context.Accesses.SingleOrDefault(p => p.AccId == accountAddVM.ctrAccess).AccId,
                    CtrEmail = accountAddVM.ctrEmail,
                    CtrAddress = accountAddVM.ctrAddress,
                    CtrStatus = 1,
                    CtrUsed = 0,
                    CtrVisible = true
                };
                Cart cart = new Cart
                {
                    CartId = customer.CtrId,
                    CartQuantity = 0

                };
                Address address = new Address
                {
                    AddId = _context.Addresses.Count(),
                    CtrId = customer.CtrId,
                    AddressName = accountAddVM.ctrAddress,
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();

                _context.Carts.Add(cart);
                _context.SaveChanges();

                _context.Addresses.Add(address);
                _context.SaveChanges();
                TempData["msg"] = "<script>alert('Successfully added, redirect to Staff Manage');</script>";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult EditAccount(AccountAddVM vM)
        {
            var check = _context.Customers.SingleOrDefault(p => p.CtrId == vM.idForUpdate);
            if (check == null)
            {
                TempData["msg"] = "<script>alert('Error: user does not exist');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                check.CtrUsername = vM.ctrUsername;
                check.CtrPassword = vM.ctrPassword;
                check.CtrGender = vM.ctrGender;
                check.CtrAccess = vM.ctrAccess;
                check.CtrEmail = vM.ctrEmail;
                check.CtrPhonenumber = vM.ctrPhonenumber;
                check.CtrAddress = vM.ctrAddress;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["msg"] = "<script>alert('Successfully edited, redirecting to Staff Manage');</script>";

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AccDelete(AccountAddVM vM)
        {
            var check = _context.Customers.SingleOrDefault(p => p.CtrId == vM.idForUpdate);
            if (check == null)
            {
                TempData["msg"] = "<script>alert('Error: user does not exist');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                //_context.RemoveRange(_context.OrdDetails.Where(p => p.OrdId == _context.Ords.SingleOrDefault(x => x.CartId == check.CtrId).OrdId));
                //_context.RemoveRange(_context.Ords.Where(p => p.CartId == check.CtrId));
                //_context.RemoveRange(_context.Carts.Where(p => p.CartId == check.CtrId));
                //_context.RemoveRange(_context.Addresses.Where(p => p.CtrId == check.CtrId));

                //_context.SaveChanges();
                //_context.Remove(check);
                //_context.SaveChanges()
                check.CtrVisible = false;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["msg"] = "<script>alert('Change succesfully');</script>";
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Restrict(AccountAddVM vM)
        {
            var check = _context.Customers.SingleOrDefault(p => p.CtrId == vM.idForUpdate);
            if (check == null)
            {
                TempData["msg"] = "<script>alert('Error: user does not exist');</script>";
                return RedirectToAction("Index");
            }
            else
            {
                check.CtrStatus = vM.ctrStatus;
                _context.Update(check);
                _context.Entry(check).State = EntityState.Modified;
                _context.SaveChanges();
            }
            TempData["msg"] = "<script>alert('Successfully updated!');</script>";
            return RedirectToAction("Index");

        }
        public IActionResult Statistic()
        {
            int TotalWA = 0;
            int RegisteredAcc = _context.Customers.Count();
            decimal TotalRevenue = 0;
            int TotalAcptOrder = _context.Ords.Where(p => p.OrdStatus == 1).Count();
            int TotalPrd = _context.Products.Count();
            foreach (WebAccess item in _context.WebAccesses)
            {
                TotalWA += item.WaCount;
            }
            foreach (Ord item in _context.Ords)
            {
                TotalRevenue += (decimal)item.OrdPrice;
            }
            StatisticsVM vm = new StatisticsVM
            {
                TotalWA = TotalWA,
                RegisteredAcc = RegisteredAcc,
                TotalRevenue = TotalRevenue,
                TotalAcptOrder = TotalAcptOrder,
                TotalPrd = TotalPrd
            };


            TempData["Check"] = "Statistic";
            return View(vm);
        }

        public string GetMonthName(int month)
        {
            DateTime dateTime = new DateTime(2024, month, 1);
            return dateTime.ToString("MMMM");
        }
        [HttpGet]
        public IActionResult GetRevenueData(int year)
        {
            List<StatisticColumnVM> columns = new List<StatisticColumnVM>();
            for (int i = 1; i <= 12; i++)
            {
                var check = _context.Ords.Where(p => p.OrdDate.Value.Month == i);
                decimal total = 0;
                foreach (var item in check)
                {
                    total += (decimal)item.OrdPrice;
                }
                StatisticColumnVM columnVM = new StatisticColumnVM
                {
                    Month = GetMonthName(i),
                    total = total,
                };
                columns.Add(columnVM);
            }
            var ls = columns;
            return Json(ls);
        }

        [HttpGet]
        public IActionResult GetWebAccessData(int year)
        {
            List<WebAccessListModel> list = new List<WebAccessListModel>();
            for (int i = 1; i <= 12; i++)
            {
                var check = _context.WebAccesses.Where(p => p.WaDate.Month == i && p.WaDate.Year == year);
                if (check != null)
                {
                    int count = 0;
                    foreach (WebAccess item in check)
                    {
                        count += item.WaCount;
                    }
                    WebAccessListModel model = new WebAccessListModel
                    {
                        Month = GetMonthName(i),
                        Count = count
                    };
                    list.Add(model);
                }
            }
            var ls = list;
            return Json(ls);
        }
        [HttpGet]
        public IActionResult GetPrdOrdTimeData(int year)
        {
            List<WebAccessListModel> columns = new List<WebAccessListModel>();
            if (_context.Ords.Any(p => p.OrdDate.Value.Year == year) == true)
            {

                var list = _context.Products.ToList();
                foreach (Product item in list) //product prdid =...
                {
                    int count = 0;
                    var cd = _context.OrdDetails.Where(p => p.PrdId == item.PrdId).ToList(); //list cd voi prdid
                    foreach (OrdDetail item2 in cd)
                    {
                        if (_context.Ords.Any(p => p.OrdId == item2.OrdId && p.OrdDate.Value.Year == year))
                            count += (int)item2.Quantity;

                    }
                    WebAccessListModel vM = new WebAccessListModel
                    {
                        Month = item.PrdName,
                        Count = count
                    };
                    columns.Add(vM);
                }
            }
            var ls = columns.OrderByDescending(p => p.Count).Take(5);
            return Json(ls);
        }
        [HttpGet]
        public IActionResult GetOrdCountData(int year)
        {
            List<OrdStatisticsModel> list = new List<OrdStatisticsModel>();
            for (int i = 1; i <= 12; i++)
            {
                var mainlist = _context.Ords.Where(p => p.OrdDate.Value.Month == i);
                int accept = mainlist.Where(p => p.OrdStatus == 1).Count();
                int denied = mainlist.Where(p => p.OrdStatus == 0).Count();
                OrdStatisticsModel ordStatisticsModel = new OrdStatisticsModel
                {
                    Month = GetMonthName(i),
                    Accepted = accept,
                    Denied = denied
                };
                list.Add(ordStatisticsModel);
            }
            var ls = list;
            return Json(ls);
        }
    }
}
