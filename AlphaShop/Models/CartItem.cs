namespace AlphaShop.Models
{
    public class CartItem
    {
        public int CartId { get; set; }

        public int PrdId { get; set; }
        public string PrdName { get; set; }
        public int? Quantity { get; set; }

        public int OptionSize { get; set; }
        public double? Price { get; set; }  
        
    }
}
