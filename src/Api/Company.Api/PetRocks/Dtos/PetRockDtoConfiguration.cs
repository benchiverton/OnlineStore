using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Api.PetRocks.Dtos;

public class PetRockDtoConfiguration : IEntityTypeConfiguration<PetRockDto>
{
    public void Configure(EntityTypeBuilder<PetRockDto> builder)
    {
        builder.Property(e => e.VariantTypes)
            .IsRequired()
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => v == null
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions())
            );
        builder.Property(e => e.Images)
            .IsRequired()
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => v == null
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions())
            );
    }
}
