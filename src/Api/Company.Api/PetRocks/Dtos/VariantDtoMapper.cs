using System;
using Company.Contract;

namespace Company.Api.PetRocks.Dtos;

public static class VariantDtoMapper
{
    public static VariantDto ToVariantDto(this Variant variant)
        => new VariantDto
        {
            Id = Guid.Parse(variant.VariantId),
            PetRockId = Guid.Parse(variant.PetRockId),
            VariantTypeValues = variant.VariantTypeValues
        };

    public static Variant FromVariantDto(this VariantDto variantDto)
        => new Variant(
            variantDto.PetRockId.ToString(),
            variantDto.Id.ToString(),
            variantDto.VariantTypeValues);
}
