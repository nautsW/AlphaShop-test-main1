using AlphaShop.Data;
using AlphaShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaShop.Controllers
{
    public class CategoryController : Controller
    {
        HahaContext objModel = new HahaContext();
        private readonly ProductModel objproductModel = new ProductModel();

        public IActionResult Index()
        {
            var lstCategory = objModel.Categories.ToList();
            var lstProduct = objModel.Products.ToList();

            Product_Category objProduct_Category = new Product_Category();
            objProduct_Category.categories = lstCategory;
            objProduct_Category.products = lstProduct;
            return View(objProduct_Category);
        }
        //[HttpPost]
        //public IActionResult Index(Product product)
        //{
        //    objproductModel.CgrId = product.CgrId;
        //    objproductModel.PrdName = product.PrdName;
        //    objproductModel.PrdId = product.PrdId;
        //    objproductModel.PrdImage = product.PrdImage;
        //    objproductModel.PrdDaubuoi = 1;
        //    return RedirectToAction("DrinkInfo");
        //}

        public IActionResult DrinkInfo(int? id)
        {
            var product = objModel.Products.FirstOrDefault(x => x.PrdId == id);
            //objModel.Entry(product).State = EntityState.Modified;
            if (product != null)
            {
                var comment = objModel.Comments.Where(p => p.PrdId == id);
                List<Comment> comments = comment.ToList();
                if (comment != null)
                {
                    //objModel.Entry(comment).State = EntityState.Modified
                    foreach (var item in comments)
                    {
                        item.Ctr = objModel.Customers.SingleOrDefault(p => p.CtrId == item.CtrId);
                    }
                    product.Comments = comments;
                }
            }
            ProductModel objProduct = new ProductModel
            {
                product = product,
                option_size = 0,
                option_type = 0,
                comment = new CommentModel()
            };

            return View(objProduct);
        }
        [HttpPost]
        public IActionResult AddComment(CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                Comment cmt = new Comment
                {
                    CmtDate = DateTime.Now,
                    CommentText = commentModel.Comment,
                    CtrId = commentModel.CtrId,
                    PrdId = commentModel.PrdId,
                    Upvote = 0
                };
                objModel.Comments.Add(cmt);
                objModel.SaveChanges();

            }

            return RedirectToAction("DrinkInfo", new { id = commentModel.PrdId });
        }
    }
}
