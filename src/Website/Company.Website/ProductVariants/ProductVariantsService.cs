using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.ProductVariants;

public class ProductVariantsService
{
    public Task<ProductVariants> GetProductVariants(string productId) => Task.FromResult(_productVariantsList.First(pt => pt.ProductId == productId));

    public Task<ProductVariant> GetProductVariant(string productId, string index1, string index2, string index3) =>
        Task.FromResult(_productVariantList.FirstOrDefault(pv =>
            pv.ProductId == productId
            && pv.Index1 == index1
            && pv.Index2 == index2
            && pv.Index3 == index3));

    public Task<ProductVariant> GetProductVariantById(string productId, int productVariantId) =>
        Task.FromResult(_productVariantList.FirstOrDefault(pv =>
            pv.ProductId == productId
            && pv.Id == productVariantId));

    private readonly List<ProductVariants> _productVariantsList = new List<ProductVariants>
        {
            new ProductVariants
            {
                ProductId = "the_product_one",
                Index1 = "Size",
                Index1Values = new List<string>
                {
                    "Small",
                    "Large"
                },
                Index2 = "Colour",
                Index2Values = new List<string>
                {
                    "Green",
                    "Greener",
                    "Greenest"
                }
            },
            new ProductVariants
            {
                ProductId = "the_product_two",
                Index1 = "Magnitude",
                Index1Values = new List<string>
                {
                    "Big",
                    "Bigger"
                },
                Index2 = "Emotion",
                Index2Values = new List<string>
                {
                    "Happy",
                    "Excited",
                    "Flattered"
                }
            }
        };

    private readonly List<ProductVariant> _productVariantList = new List<ProductVariant>
        {
            new ProductVariant
            {
                Id = 0,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Green"
            },
            new ProductVariant
            {
                Id = 1,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Greener"
            },
            new ProductVariant
            {
                Id = 2,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Small",
                Index2 = "Greenest"
            },
            new ProductVariant
            {
                Id = 10,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Green"
            },
            new ProductVariant
            {
                Id = 11,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Greener"
            },
            new ProductVariant
            {
                Id = 12,
                ProductId = "the_product_one",
                Stock = 1,
                Index1 = "Large",
                Index2 = "Greenest"
            },
            new ProductVariant
            {
                Id = 20,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Big",
                Index2 = "Happy"
            },
            new ProductVariant
            {
                Id = 21,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Big",
                Index2 = "Excited"
            },
            new ProductVariant
            {
                Id = 22,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Big",
                Index2 = "Flattered"
            },
            new ProductVariant
            {
                Id = 30,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Bigger",
                Index2 = "Happy"
            },
            new ProductVariant
            {
                Id = 31,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Bigger",
                Index2 = "Excited"
            },
            new ProductVariant
            {
                Id = 32,
                ProductId = "the_product_two",
                Stock = 1,
                Index1 = "Bigger",
                Index2 = "Flattered"
            },
        };
}
