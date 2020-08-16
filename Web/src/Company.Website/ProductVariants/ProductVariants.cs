using System.Collections.Generic;

namespace Company.Website.ProductVariants
{
    public class ProductVariants
    {
        public string ProductId { get; set; }

        public string Index1 { get; set; }
        public List<string> Index1Values { get; set; }
        public string Index2 { get; set; }
        public List<string> Index2Values { get; set; }
        public string Index3 { get; set; }
        public List<string> Index3Values { get; set; }
    }
}
