using AlphaShop.Data;

namespace AlphaShop.Models
{
    public class OrderModel
    {
        public int quantity { get; set; }
        public decimal? Total { get; set; }
        public string? note { get; set; }
        public string? destination { get; set; }
        public decimal? Shipping { get; set; }
        public Customer? customer { get; set; }
        public CartModel? Cart { get; set; }

    }
}
