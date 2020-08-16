using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Website.ProductInformation
{
    public class ProductInformationService
    {
        public async Task<ProductInformation> GetProductInformationById(string productId) => _productInformation[productId];

        private readonly Dictionary<string, ProductInformation> _productInformation = new Dictionary<string, ProductInformation>
        {
            { "the_product_one", new ProductInformation
            {
                Name = "The Product One",
                Description1 = "A great product for great people",
                Description2 = "Improve your well-being with Product One"
            }
            },
            { "the_product_two", new ProductInformation
            {
                Name = "The Product Two",
                Description1 = "A brilliant product for brilliant people",
                Description2 = "Improve your mindfulness with Product Two"
            } 
            }
        };
    }
}
