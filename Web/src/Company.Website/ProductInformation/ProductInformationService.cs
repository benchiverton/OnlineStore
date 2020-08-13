using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Website.ProductInformation
{
    public class ProductInformationService
    {
        public async Task<ProductInformation> GetProductInformationById(string productId)
        {
            return _productInformation[productId];
        }

        private readonly Dictionary<string, ProductInformation> _productInformation = new Dictionary<string, ProductInformation>
        {
            { "the_mushy_bed", new ProductInformation
            {
                Name = "The Mushy Bed",
                Description1 = "Anti-Anxiety Dog Bed",
                Description2 = "Improve Your Dogs Health & Well-Being"
            }
            },
            { "the_mushy_blanket", new ProductInformation
            {
                Name = "The Mushy Blanket",
                Description1 = "Anti-Anxiety Dog Blanket",
                Description2 = "Improve Your Dogs Health & Well-Being"
            } 
            }
        };
    }
}
