using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace AlphaShop.Controllers
{
    public class LogController : Controller
    {
        private readonly HahaContext _context;
        private readonly AccountService _accountService;

        public LogController(HahaContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService; // Lưu trữ reference đến AccountService
        }

        //public IActionResult Login()
        //{
        //    if(_accountService.IsLoggedIn == true)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}
        //[HttpPost]
        //[HttpPost]
        //public IActionResult Login(LoginModel loginModel)
        //{
        //    var taikhoanForm = loginModel.Name;
        //    var matkhauForm = loginModel.Password;
        //    var usercheck = _context.Customers.SingleOrDefault(x => x.CtrLogusername == taikhoanForm && x.CtrPassword == matkhauForm);
        //    if (usercheck != null)
        //    {
        //        _accountService.IsLoggedIn = true;
        //        _accountService.Customer = usercheck;
        //        _accountService.Customer.Cart = _context.Carts.SingleOrDefault(x => x.CartId == usercheck.CtrId);
        //        _accountService.Customer.Cart.CartDetails = _context.CartDetails.Where(x => x.CartId == usercheck.CtrId).ToList();
        //        foreach(CartDetail a in  _context.CartDetails)
        //        {
        //            _accountService.Customer.Cart.CartQuantity += a.Quantity;
        //        }    
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.LoginFail = "Failed to log";
        //        return View();
        //    }
        //}
        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var taikhoanForm = loginModel.Name;
            var matkhauForm = loginModel.Password;
            var usercheck = _context.Customers.SingleOrDefault(x => x.CtrLogusername == taikhoanForm && x.CtrPassword == matkhauForm);
            if (usercheck != null && usercheck.CtrVisible == true)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usercheck.CtrUsername.ToString()),
                    new Claim(ClaimTypes.Role, usercheck.CtrAccess.ToString()),
                    new Claim("CtrId", usercheck.CtrId.ToString()),


                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                };
                //singleton cũ

                //singleton cũ
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.LoginFail = "Failed to log";
            return View();
        }

        public IActionResult Register()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var usercheck = _context.Customers.SingleOrDefault(x => x.CtrLogusername == registerModel.LogUsername);
            if (usercheck != null)
            {
                ViewBag.RegisterFailed = "Failed to register, username has existed!";
                return View();
            }
            else if (registerModel.Password != registerModel.ConfirmPassword)
            {
                ViewBag.RegisterFailed = "Password is not synced";
                return View();
            }

            //await _context.Database.ExecuteSqlRawAsync($"set IDENTITY_INSERT dbo.CUSTOMER on;");
            Customer customer = new Customer
            {
                CtrLogusername = registerModel.LogUsername,
                CtrUsername = registerModel.Username,
                CtrPassword = registerModel.Password,
                CtrEmail = registerModel.Email,
                CtrPhonenumber = registerModel.PhoneNumber,
                CtrGender = registerModel.Gender,
                CtrAccess = 1,
                CtrStatus = 1,
                CtrAddress = registerModel.Address,
                Addresses = new HashSet<Address>(),
                CtrId = _context.Customers.Count(),
                CtrUsed = 0,
                CtrImage = null,
                CtrVisible = true


            };
            
            
            Address newadd = new Address
            {
                CtrId = customer.CtrId,
                AddId = 1,
                AddressName = registerModel.Address
            };
            //_context.Customers.FirstOrDefault(x=> x.CtrId == customer.CtrId).Addresses.Add(newadd);
            customer.Addresses.Add(newadd);
            await _context.Customers.AddAsync(
                customer
            );
            await _context.Carts.AddAsync(
            new Cart
            {
                CartId = customer.CtrId,
                CartQuantity = 0,
            }
            );
            _context.SaveChanges();
            //await _context.Database.ExecuteSqlRawAsync($"set IDENTITY_INSERT dbo.CUSTOMER off;");
            _context.Database.BeginTransaction();
            return RedirectToAction("Login");
        }
        //public IActionResult Logout()
        //{
        //    _accountService.IsLoggedIn = false;
        //    _accountService.Customer = null;
        //    return RedirectToAction("Index", "Home");
        //}

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Log");
        }
    }
}