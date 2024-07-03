using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Api.Adoption.Dtos
{
    public class AdoptableRockVariantDtoConfiguration : IEntityTypeConfiguration<AdoptableRockVariantDto>
    {
        public void Configure(EntityTypeBuilder<AdoptableRockVariantDto> builder)
        {
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
}
