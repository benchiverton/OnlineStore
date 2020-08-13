using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.ProductVarients
{
    public class ProductVarientsService
    {
        public async Task<ProductVarients> GetProductVarients(string productId)
        {
            return _productVarientsList.First(pt => pt.ProductId == productId);
        }

        public async Task<ProductVarient> GetProductVarient(string productId, string index1, string index2, string index3)
        {
            return _productVarientList.FirstOrDefault(pv =>
                pv.ProductId == productId
                && pv.Index1 == index1
                && pv.Index2 == index2
                && pv.Index3 == index3);
        }

        private readonly List<ProductVarients> _productVarientsList = new List<ProductVarients>
        {
            new ProductVarients
            {
                ProductId = "the_mushy_bed",
                Index1 = "Size",
                Index1Values = new List<string>
                {
                    "Small",
                    "Medium",
                    "Large"
                },
                Index2 = "Colour",
                Index2Values = new List<string>
                {
                    "Caramel",
                    "Light Grey",
                    "Dark Grey"
                }
            }
        };

        private readonly List<ProductVarient> _productVarientList = new List<ProductVarient>
        {
            new ProductVarient
            {
                Id = 0,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Caramel"
            },
            new ProductVarient
            {
                Id = 1,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Light Grey"
            },
            new ProductVarient
            {
                Id = 2,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Dark Grey"
            },
            new ProductVarient
            {
                Id = 10,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Medium",
                Index2 = "Caramel"
            },
            new ProductVarient
            {
                Id = 11,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Medium",
                Index2 = "Light Grey"
            },
            new ProductVarient
            {
                Id = 12,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Medium",
                Index2 = "Dark Grey"
            },
            new ProductVarient
            {
                Id = 20,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Caramel"
            },
            new ProductVarient
            {
                Id = 21,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Light Grey"
            },
            new ProductVarient
            {
                Id = 22,
                ProductId = "the_mushy_bed",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Dark Grey"
            },
        };
    }
}
