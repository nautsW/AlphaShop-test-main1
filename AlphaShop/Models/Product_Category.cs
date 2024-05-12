using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class Product_Category
    {
        public string? searchString {  get; set; }
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
    }
}
