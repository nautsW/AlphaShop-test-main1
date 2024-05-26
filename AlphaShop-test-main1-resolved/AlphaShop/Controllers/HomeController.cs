using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AlphaShop.Models;
using AlphaShop.Data;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HahaContext _context;

    public HomeController(ILogger<HomeController> logger)
    {
        _context = new HahaContext();
        _logger = logger;
    }

    public IActionResult Index()
    {

        DateTime date = DateTime.Now.Date;
        var check = _context.WebAccesses.SingleOrDefault(p => p.WaDate == date);
        if (check == null)
        {
            WebAccess a = new WebAccess
            {
                WaCount = 1,
                WaDate = date,
            };
            _context.WebAccesses.Add(a);
            _context.SaveChanges();
        }
        else
        {
            check.WaCount++;
            _context.Entry(check).State = EntityState.Modified;
            _context.SaveChanges();
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
