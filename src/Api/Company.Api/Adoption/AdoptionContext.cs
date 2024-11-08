using System;
using System.Collections.Generic;
using Company.Api.Adoption.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Adoption;

public class AdoptionContext : DbContext
{
    public DbSet<AdoptableRockDto> RocksUpForAdoption { get; set; }
    public DbSet<PetRockDto> PetRocks { get; set; }

    public AdoptionContext(DbContextOptions<AdoptionContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdoptableRockDtoConfiguration());
        modelBuilder.ApplyConfiguration(new PetRockDtoConfiguration());

        var petRock1 = new AdoptableRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Pebble Dash",
            Catchphrase = "Rolling through life one pebble at a time!",
            Description =
                "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.",
            CustomisableAttributes = new Dictionary<string, List<string>>
            {
                { "Size", ["Small", "Big"] }, { "Texture", ["Smooth", "Grainy", "Jagged"] }
            },
            Images = [@"images\petrocks\pebble_dash.png"]
        };
        var petRock2 = new AdoptableRockDto
        {
            Id = Guid.NewGuid(),
            Name = "Slate Mate",
            Catchphrase = "Ready to rock and slate the day!",
            Description =
                "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!",
            CustomisableAttributes = new Dictionary<string, List<string>>
            {
                { "Hardness", ["Soft", "Hard"] }, { "Slateyness", ["Not very", "Moderately", "Very"] }
            },
            Images = [@"images\petrocks\slate_mate.png"]
        };
        modelBuilder.Entity<AdoptableRockDto>().HasData(petRock1, petRock2);
    }
}
