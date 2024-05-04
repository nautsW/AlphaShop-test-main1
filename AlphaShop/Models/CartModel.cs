using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class CartModel
    {
        public Cart? cart { get; set; }
        public List<CartDetail>? cartDetail { get; set; }
    }
}
