using AlphaShop.Data;

namespace AlphaShop.Models
{
    //public class ProductModel
    //{
    //    public Product? product { get; set; }
    //    public int option_type { get; set; }
    //    public int option_size { get; set; }

    //}
    public class ProductModel
    {
        public Product? product { get; set; }
        public int option_type { get; set; }
        public int option_size { get; set; }
        public CommentModel? comment { get; set; }

    }
    public class CommentModel
    {
        public string Comment { get; set; }
        public int PrdId { get; set; }
        public int CtrId { get; set; }
    }
}
