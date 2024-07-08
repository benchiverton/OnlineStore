using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Api.Adoption.Dtos
{
    public class AdoptableRockDtoConfiguration : IEntityTypeConfiguration<AdoptableRockDto>
    {
        public void Configure(EntityTypeBuilder<AdoptableRockDto> builder)
        {
            builder.Property(e => e.CustomisableAttributes)
                .IsRequired()
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => v == null
                        ? new Dictionary<string, List<string>>()
                        : JsonSerializer.Deserialize<Dictionary<string, List<string>>>(v, new JsonSerializerOptions())
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
}
