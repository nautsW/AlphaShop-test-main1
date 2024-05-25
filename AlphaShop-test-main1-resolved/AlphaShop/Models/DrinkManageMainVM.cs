using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class DrinkManageMainVM
    {
        public DrinkAddVM? addModel { get; set; }
        public CategoryAddVM? addCategory { get; set; }
        public List<Product>? products { get; set; }
        public string? searchString { get; set; }
    }
}
