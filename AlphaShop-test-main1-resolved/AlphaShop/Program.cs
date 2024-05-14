using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<AccountService>();
builder.Services.AddControllersWithViews();

//test authen
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = "/Log/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    }
    );
//test authen

builder.Services.AddDbContext<HahaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PBL3") ?? throw new InvalidOperationException("Connection string 'PBL3' not found.")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//test authen
app.UseAuthentication();
//test authen
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "category",
//    pattern: "category",
//    defaults: new { controller = "GuestMenu", action = "Category" });

//app.MapControllerRoute(
//    name: "order",
//    pattern: "order",
//    defaults: new { controller = "GuestMenu", action = "Order" });

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=GuestMenu}/{action=Index}/{id?}");


//app.MapControllerRoute(
//    name: "login",
//    pattern: "login",
//    defaults: new { controller = "GuestMenu", action = "Login" });

//app.MapControllerRoute(
//    name: "register",
//    pattern: "register",
//    defaults: new { controller = "GuestMenu", action = "Register" });

//app.MapControllerRoute(
//    name: "cartinfo",
//    pattern: "cartinfo",
//    defaults: new { controller = "GuestMenu", action = "Cartinfo" });

//app.MapControllerRoute(
//    name: "userinfo",
//    pattern: "userinfo",
//    defaults: new { controller = "GuestMenu", action = "Userinfo" });

//app.MapControllerRoute(
//    name: "drinkinfo",
//    pattern: "drinkinfo",
//    defaults: new { controller = "GuestMenu", action = "Drinkinfo" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//test authen
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Log}/{action=Login}/{id?}");
//test authen
app.Run();