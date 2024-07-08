using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Api.Adoption.Dtos
{
    public class PetRockDtoConfiguration : IEntityTypeConfiguration<PetRockDto>
    {
        public void Configure(EntityTypeBuilder<PetRockDto> builder)
        {
            builder.HasIndex(e => e.OwnerId);

            builder.Property(e => e.Attributes)
                .IsRequired()
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => v == null
                        ? new Dictionary<string, string>()
                        : JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions())
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
