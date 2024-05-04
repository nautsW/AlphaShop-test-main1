﻿using AlphaShop.Data
;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var taikhoanForm = loginModel.Name;
            var matkhauForm = loginModel.Password;
            var usercheck = _context.Customers.SingleOrDefault(x => x.CtrLogusername == taikhoanForm && x.CtrPassword == matkhauForm);
            if (usercheck != null)
            {
                _accountService.IsLoggedIn = true;
                _accountService.Customer = usercheck;
                _accountService.Customer.Cart = _context.Carts.SingleOrDefault(x => x.CartId == usercheck.CtrId);
                _accountService.Customer.Cart.CartDetails = _context.CartDetails.Where(x => x.CartId == usercheck.CtrId).ToList();

                

                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginFail = "Failed to log";
                return View();
            }
        }


        public IActionResult Register()
        {
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
            await _context.Customers.AddAsync(
                new Customer
                {
                    CtrLogusername = registerModel.LogUsername,
                    CtrUsername = registerModel.Username,
                    CtrPassword = registerModel.Password,
                    CtrEmail = registerModel.Email,
                    CtrPhonenumber = registerModel.PhoneNumber,
                    CtrGender = registerModel.Gender,
                    CtrAccess = 1,
                    CtrId = _context.Customers.Count(),
                    CtrUsed = 0,
                    CtrImage = null


                }
            );
            _context.SaveChanges();
            //await _context.Database.ExecuteSqlRawAsync($"set IDENTITY_INSERT dbo.CUSTOMER off;");
            _context.Database.BeginTransaction();
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            _accountService.IsLoggedIn = false;
            _accountService.Customer = null;
            _accountService.Customer.Cart = null;
            _accountService.Customer.Cart.CartDetails = null;
            return RedirectToAction("Index", "Home");
        }
    }
}