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
                "Small, smooth, and full of spirit, this round little explorer is always up for an adventure. Whether it’s zipping across your desk or chilling on your nightstand, Pebble Dash brings charm and good vibes wherever it rolls. Pocket-sized joy for everyday journeys.",
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
                "Smooth, sleek, and always chill, Slate Mate brings modern style and steady vibes to your space. Whether you're grinding through work or just vibing, this flat little legend’s got your back. Rock your day with Slate Mate – the solid sidekick you didn’t know you needed.",
            CustomisableAttributes = new Dictionary<string, List<string>>
            {
                { "Hardness", ["Soft", "Hard"] }, { "Slateyness", ["Not very", "Moderately", "Very"] }
            },
            Images = [@"images\petrocks\slate_mate.png"]
        };
        modelBuilder.Entity<AdoptableRockDto>().HasData(petRock1, petRock2);
    }
}
