using System;
using System.Collections.Generic;
using Company.Api.Adoption.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Adoption;

public class AdoptionContext : DbContext
{
    public DbSet<AdoptableRockDto> RocksUpForAdoption { get; set; }
    public DbSet<AdoptableRockVariantDto> RockVariantsUpForAdoption { get; set; }
    public DbSet<PetRockDto> PetRocks { get; set; }

    public AdoptionContext(DbContextOptions<AdoptionContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdoptableRockDtoConfiguration());
        modelBuilder.ApplyConfiguration(new AdoptableRockVariantDtoConfiguration());
        modelBuilder.ApplyConfiguration(new PetRockDtoConfiguration());

        var petRock1 = new AdoptableRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Pebble Dash",
            Catchphrase = "Rolling through life one pebble at a time!",
            Description = "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.",
            VariantTypes = ["Size", "Texture"],
            Images = [@"images\petrocks\pebble_dash.png"]
        };
        var petRock2 = new AdoptableRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Slate Mate",
            Catchphrase = "Ready to rock and slate the day!",
            Description = "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!",
            VariantTypes = ["Hardness", "Slateyness"],
            Images = [@"images\petrocks\slate_mate.png"]
        };
        modelBuilder.Entity<AdoptableRockDto>().HasData(petRock1, petRock2);

        var variant1 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Smooth" }, },
        };
        var variant2 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Grainy" }, },
        };
        var variant3 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Jagged" }, },
        };
        var variant4 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Smooth" }, },
        };
        var variant5 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Grainy" }, },
        };
        var variant6 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Jagged" }, },
        };
        var variant7 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Not very" },
            },
        };
        var variant8 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Moderately slatey" },
            },
        };
        var variant9 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Extra slatey" },
            },
        };
        var variant10 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Not very" },
            },
        };
        var variant11 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Moderately slatey" },
            },
        };
        var variant12 = new AdoptableRockVariantDto
        {
            Id = Guid.NewGuid(),
            AdoptableRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Extra slatey" },
            },
        };
        modelBuilder.Entity<AdoptableRockVariantDto>().HasData(variant1, variant2, variant3, variant4, variant5, variant6, variant7, variant8, variant9, variant10, variant11, variant12);
    }
}
