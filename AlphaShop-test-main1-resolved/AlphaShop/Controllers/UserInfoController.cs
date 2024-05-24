using Microsoft.AspNetCore.Mvc;
using AlphaShop.Models;
using Microsoft.AspNetCore.Hosting;
using AlphaShop.Data;
using NuGet.Versioning;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AlphaShop.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly HahaContext _context;
        private readonly AccountService _accountService;
        private readonly IWebHostEnvironment _environment;
        //private readonly int CtrId_global;
        public UserInfoController(HahaContext context, AccountService accountService, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            //CtrId_global = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
        }
        public IActionResult Index()
        {
            int CtrId_global = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            Customer customer = _context.Customers.SingleOrDefault(p => p.CtrId == CtrId_global);
            customer.Addresses = _context.Addresses.Where(p => p.CtrId == CtrId_global).ToList();
            UserInfoVM userInfoVM = new UserInfoVM
            {
                ChangePassword = new ChangePasswordModel(),
                imageModel = new ChangeImageModel(),
                customer = customer,
            };

            return View(userInfoVM);
        }

        public IActionResult OrderList()
        {
            int CartId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            List<Ord> ords = _context.Ords.Where(x => x.CartId == CartId).ToList();
            return View(ords);
        }

        [HttpPost]
        public IActionResult ReplacePassword(ChangePasswordModel model)
        {
            int CartId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
            var user = _context.Customers.SingleOrDefault(x => x.CtrPassword == model.OldPassword && x.CtrId == CartId);

            if (user != null)
            {
                // Kiểm tra xác nhận mật khẩu mới
                if (model.NewPassword == model.ConfirmNewPassword)
                {
                    // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                    user.CtrPassword = model.NewPassword;
                    _context.SaveChanges();

                    TempData["Alert"] = "<script>alert('Mật khẩu đã được thay đổi thành công!')</script>";

                    // Cập nhật lại claim "SerialNumber" (mật khẩu) trong mã điều khiển
                }
                else
                {
                    TempData["Alert"] = "<script>alert('Mật khẩu mới không khớp!')</script>";
                    
                }
            }
            else
            {
                TempData["Alert"] = "<script>alert('Mật khẩu cũ không đúng!')</script>";
                
            }
            return RedirectToAction("Index", "UserInfo");
        }

        [HttpPost]
        public IActionResult ChangeImage(ChangeImageModel model)
        {
            try
            {
                int CartId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId").Value);
                var user = _context.Customers.SingleOrDefault(x => x.CtrId == CartId);
                if (model.Image != null && model.Image.Length > 0)
                {
                    // Kiểm tra xem trường input ẩn "ImageFolderPath" có chứa đường dẫn thư mục lưu trữ ảnh không
                    if (!string.IsNullOrEmpty(model.ImageFolderPath))
                    {
                        // Lấy đường dẫn thư mục lưu trữ ảnh từ trường ẩn
                        string imageFolderPath = Path.Combine(_environment.WebRootPath, "assets", "img", "pfp");
                        string fileName = Path.GetFileName(model.Image.FileName);
                        // Tạo đường dẫn tới tệp ảnh mới bằng cách kết hợp đường dẫn thư mục và tên tệp ảnh
                        string filePath = Path.Combine(imageFolderPath, fileName);
                        string relativefilePath = filePath.Substring(filePath.IndexOf("assets"));

                        // Kiểm tra xem thư mục đích có tồn tại không
                        if (!Directory.Exists(imageFolderPath))
                        {
                            // Nếu không tồn tại, tạo thư mục đích
                            Directory.CreateDirectory(imageFolderPath);
                        }

                        // Lưu trữ tệp ảnh

                        // Lưu đường dẫn tới ảnh vào cơ sở dữ liệu hoặc làm bất kỳ điều gì bạn cần
                        // ...
                        user.CtrImage = relativefilePath;
                        /*_accountService.Customer.CtrImage = relativefilePath;*/
                        _context.SaveChanges();

                        //TempData["Alert"] = "<script>alert('Ảnh đã được thay đổi thành công')</script>";
                    }
                    else
                    {
                        TempData["Alert"] = "<script>alert('Không có đường dẫn thư mục lưu trữ ảnh!')</script>";
                        return RedirectToAction("Index", "UserInfo");
                    }
                }
                else
                {
                    TempData["Alert"] = "<script>alert('Không có tệp ảnh được chọn!')</script>";
                    return RedirectToAction("Index", "UserInfo");
                }
            }
            catch (Exception ex)
            {
                TempData["Alert"] = $"<script>alert('{ex}')</script>";
                return RedirectToAction("Index", "UserInfo");

            }
            TempData["Alert"] = "<script>alert('Đổi thành công')</script>";

            return RedirectToAction("Index", "UserInfo");
        }

        [HttpPost]
        public IActionResult OrderDetail(int id)
        {

            Ord order = _context.Ords.SingleOrDefault(p => p.OrdId == id);
            if (order == null)
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
                order.OrdDetails = ordDetails;
            }
            Customer cus = _context.Customers.SingleOrDefault(x => x.CtrId == order.CartId);
            cus.CtrAddress = _context.Customers.SingleOrDefault(x => x.CtrId == order.CartId).CtrAddress;
            cus.CtrPhonenumber = _context.Customers.SingleOrDefault(x => x.CtrId == order.CartId).CtrPhonenumber;
            OrderInfoVM orderInfoVM = new OrderInfoVM
            {
                ord = order,
                customer = cus,
            };
            return View(orderInfoVM);
        }

        [HttpPost]
        public IActionResult AddAddress(string new_address, Address lastitem)
        {
            // Lấy CtrId từ Claims của người dùng hiện tại
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId")?.Value);

            // Kiểm tra nếu CtrId hợp lệ
            
                // Tìm khách hàng với CtrId tương ứng
                var customer = _context.Customers.SingleOrDefault(x => x.CtrId == CtrId);

                if (customer != null)
                {
                    int index = _context.Addresses.Where(x => x.CtrId == CtrId).Last().AddId + 1;
                    // Tạo đối tượng Address mới
                    Address newadd = new Address
                    {
                        CtrId = CtrId,
                        AddId = index + 1,
                        AddressName = new_address
                    };

                    // Thêm địa chỉ mới vào danh sách Addresses
                    customer.Addresses.Add(newadd);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Redirect đến trang Index của UserInfo sau khi thành công
                    return RedirectToAction("Index", "UserInfo");
                }
                else
                {
                    // Xử lý khi không tìm thấy khách hàng
                    ModelState.AddModelError(string.Empty, "Không tìm thấy khách hàng");
                }
            
            

            // Trả về view hiện tại với thông báo lỗi
            return RedirectToAction("Index", "UserInfo");
        }

        [HttpPost]
        public IActionResult SetAddress(string address)
        {
            // Lấy CtrId từ Claims của người dùng hiện tại
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId")?.Value);

            // Kiểm tra nếu CtrId hợp lệ

            // Tìm khách hàng với CtrId tương ứng
            var customer = _context.Customers.SingleOrDefault(x => x.CtrId == CtrId);
            
            

            if (customer != null)
            {
                customer.CtrAddress = address;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                // Redirect đến trang Index của UserInfo sau khi thành công
                return RedirectToAction("Index", "UserInfo");
            }
            else
            {
                // Xử lý khi không tìm thấy khách hàng
                ModelState.AddModelError(string.Empty, "Không tìm thấy khách hàng");
            }



            // Trả về view hiện tại với thông báo lỗi
            return RedirectToAction("Index", "UserInfo");
        }

        [HttpPost]
        public IActionResult DeleteAddress(int id)
        {
            // Lấy CtrId từ Claims của người dùng hiện tại
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId")?.Value);

            // Kiểm tra nếu CtrId hợp lệ
            var customer = _context.Customers.SingleOrDefault(x => x.CtrId == CtrId);
            // Tìm khách hàng với CtrId tương ứng
            var address = _context.Addresses.SingleOrDefault(x => x.CtrId == CtrId && x.AddId == id);
            var address_list = _context.Addresses;

            if (address != null && customer != null)
            {
                _context.Entry(address).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //customer.Addresses.Remove(address);
                //address_list.Remove(address);
                _context.Remove(address);
                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
                // Redirect đến trang Index của UserInfo sau khi thành công
                return RedirectToAction("Index", "UserInfo");
            }
            else
            {
                // Xử lý khi không tìm thấy khách hàng
                TempData["Alert"] = $"<script>alert('fail: {address} {customer.ToString()}');</script>";
            }



            // Trả về view hiện tại với thông báo lỗi
            return RedirectToAction("Index", "UserInfo");
        }

        [HttpPost]
        public IActionResult UpdateAddress(int id, string new_address)
        {
            // Lấy CtrId từ Claims của người dùng hiện tại
            int CtrId = Convert.ToInt32(HttpContext.User.Claims.SingleOrDefault(p => p.Type == "CtrId")?.Value);

            // Kiểm tra nếu CtrId hợp lệ

            // Tìm khách hàng với CtrId tương ứng
            var address = _context.Addresses.SingleOrDefault(x=>x.CtrId == CtrId && x.AddId == id);

            if (address != null)
            {

                address.AddressName = new_address;
                _context.Entry(address).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
                // Redirect đến trang Index của UserInfo sau khi thành công
                return RedirectToAction("Index", "UserInfo");
            }
            else
            {
                // Xử lý khi không tìm thấy khách hàng
                ModelState.AddModelError(string.Empty, "Không tìm thấy khách hàng");
            }



            // Trả về view hiện tại với thông báo lỗi
            return RedirectToAction("Index", "UserInfo");
        }





    }
}