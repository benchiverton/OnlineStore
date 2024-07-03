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

        var variant1 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Smooth" }, },
        };
        var variant2 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Grainy" }, },
        };
        var variant3 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Small" }, { "Texture", "Jagged" }, },
        };
        var variant4 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Smooth" }, },
        };
        var variant5 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Grainy" }, },
        };
        var variant6 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock1.Id,
            VariantTypeValues = new Dictionary<string, string> { { "Size", "Big" }, { "Texture", "Jagged" }, },
        };
        var variant7 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Not very" },
            },
        };
        var variant8 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Moderately slatey" },
            },
        };
        var variant9 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Soft" }, { "Slateyness", "Extra slatey" },
            },
        };
        var variant10 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Not very" },
            },
        };
        var variant11 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Moderately slatey" },
            },
        };
        var variant12 = new VariantDto
        {
            Id = Guid.NewGuid(),
            PetRockId = petRock2.Id,
            VariantTypeValues = new Dictionary<string, string>
            {
                { "Hardness", "Hard" }, { "Slateyness", "Extra slatey" },
            },
        };
        modelBuilder.Entity<VariantDto>().HasData(variant1, variant2, variant3, variant4, variant5, variant6, variant7, variant8, variant9, variant10, variant11, variant12);
    }
}
