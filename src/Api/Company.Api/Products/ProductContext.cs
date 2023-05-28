using System;
using System.Collections.Generic;
using System.Linq;
using Company.Api.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Products;

public class ProductContext : DbContext
{
    public DbSet<ProductDto> Products { get; set; }
    public DbSet<VariantDto> Variants { get; set; }

    public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductDtoConfiguration());
        modelBuilder.ApplyConfiguration(new VariantDtoConfiguration());

        var product1 = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "The Product One",
            Headline = "A great product for great people",
            Description = "Improve your well-being with Product One",
            VariantTypes = new List<string> { "Size", "Colour" },
            Images = new List<string> { @"images\products\the_product_one.png" }
        };
        var product2 = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "The Product Two",
            Headline = "A brilliant product for brilliant people",
            Description = "Improve your mindfulness with Product Two",
            VariantTypes = new List<string> { "Magnitude", "Emotion" },
            Images = new List<string> { @"images\products\the_product_two.png" }
        };
        modelBuilder.Entity<ProductDto>().HasData(product1, product2);

        var rand = new Random();
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Colour", "Green" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Colour", "Greener" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Colour", "Greenest" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Colour", "Green" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Colour", "Greener" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Colour", "Greenest" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Small" },
                { "Emotion", "Excited" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Small" },
                { "Emotion", "Happy" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Small" },
                { "Emotion", "Flattered" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Vast" },
                { "Emotion", "Excited" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Vast" },
                { "Emotion", "Happy" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            ProductId = product2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Magnitude", "Vast" },
                { "Emotion", "Flattered" },
            },
        });
    }

    private void AddWithRandomPrice(ModelBuilder modelBuilder, Random rand, VariantDto variant)
        => modelBuilder.Entity<VariantDto>(v =>
            {
                v.HasData(variant);
                v.OwnsOne(x => x.Price).HasData(GenerateRandomPrice(variant.Id, rand));
            });


    private PriceDto GenerateRandomPrice(Guid variantId, Random rand)
    {
        var fullPrice = rand.Next(100);
        return new PriceDto
        {
            VariantDtoId = variantId,
            FullPriceGBP = fullPrice,
            DealPriceGBP = fullPrice * 0.67m,
            Details = (fullPrice % 3) switch
            {
                0 => "33% off today!",
                1 => "33% off while stocks last!",
                _ => "2/3 off for a limited time!"
            }
        };
    }
}
