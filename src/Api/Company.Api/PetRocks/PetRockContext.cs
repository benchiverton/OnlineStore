using System;
using System.Collections.Generic;
using Company.Api.PetRocks.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.PetRocks;

public class PetRockContext : DbContext
{
    public DbSet<PetRockDto> PetRocks { get; set; }
    public DbSet<VariantDto> Variants { get; set; }

    public PetRockContext(DbContextOptions<PetRockContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetRockDtoConfiguration());
        modelBuilder.ApplyConfiguration(new VariantDtoConfiguration());

        var petRock1 = new PetRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Pebble Dash",
            Catchphrase = "Rolling through life one pebble at a time.",
            Description = "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.",
            VariantTypes = ["Size", "Texture"],
            Images = [@"images\petrocks\pebble_dash.png"]
        };
        var petRock2 = new PetRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Slate Mate",
            Catchphrase = "Ready to rock and slate the day.",
            Description = "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!",
            VariantTypes = ["Hardness", "Slateyness"],
            Images = [@"images\petrocks\slate_mate.png"]
        };
        modelBuilder.Entity<PetRockDto>().HasData(petRock1, petRock2);

        var rand = new Random();
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Texture", "Smooth" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Texture", "Grainy" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Small" },
                { "Texture", "Jagged" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Texture", "Smooth" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Texture", "Grainy" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Size", "Big" },
                { "Texture", "Jagged" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" },
                { "Slateyness", "Not very" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" },
                { "Slateyness", "Moderately slatey" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" },
                { "Slateyness", "Extra slatey" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" },
                { "Slateyness", "Not very" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" },
                { "Slateyness", "Moderately slatey" },
            },
        });
        AddWithRandomPrice(modelBuilder, rand, new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" },
                { "Slateyness", "Extra slatey" },
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
