using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.Product
{
    public class ProductService
    {
        public async Task<List<Product>> GetProducts()
        {
            return _products;
        }

        public async Task<Product> GetProductById(string productId)
        {
            return _products.First(p => p.Id == productId);
        }

        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = "the_mushy_bed"
            },
            new Product
            {
                Id = "the_mushy_blanket"
            }
        };
    }
}
