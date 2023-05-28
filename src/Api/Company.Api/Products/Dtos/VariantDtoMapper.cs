using System;
using Company.Contract;

namespace Company.Api.Products.Dtos;

public static class VariantDtoMapper
{
    public static VariantDto ToVariantDto(this Variant variant)
        => new VariantDto
        {
            Id = Guid.Parse(variant.VariantId),
            ProductId = Guid.Parse(variant.ProductId),
            VariantTypeValues = variant.VariantTypeValues,
            Price = variant.Price.ToPriceDto()
        };

    public static Variant FromVariantDto(this VariantDto variantDto)
        => new Variant(
            variantDto.ProductId.ToString(),
            variantDto.Id.ToString(),
            variantDto.VariantTypeValues,
            variantDto.Price.FromPriceDto());
}
