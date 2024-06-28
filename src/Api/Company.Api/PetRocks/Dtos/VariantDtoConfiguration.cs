using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Api.PetRocks.Dtos;

public class VariantDtoConfiguration : IEntityTypeConfiguration<VariantDto>
{
    public void Configure(EntityTypeBuilder<VariantDto> builder)
    {
        builder.OwnsOne(v => v.Price).WithOwner();
        builder.Property(e => e.VariantTypeValues)
            .IsRequired()
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => v == null
                    ? new Dictionary<string, string>()
                    : JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions())
            );
    }
}
