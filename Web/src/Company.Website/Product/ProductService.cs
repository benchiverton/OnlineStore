using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.Product
{
    public class ProductService
    {
        public async Task<List<Product>> GetProducts() => _products;

        public async Task<Product> GetProductById(string productId) => _products.First(p => p.Id == productId);

        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = "the_product_one"
            },
            new Product
            {
                Id = "the_product_two"
            }
        };
    }
}
