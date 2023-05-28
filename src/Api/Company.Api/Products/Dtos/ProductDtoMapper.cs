using System;
using Company.Contract;

namespace Company.Api.Products.Dtos;

public static class ProductDtoMapper
{
    public static ProductDto ToProductDto(this Product product)
        => new ProductDto
        {
            Id = Guid.Parse(product.Id),
            Name = product.Name,
            Headline = product.Headline,
            Description = product.Description,
            VariantTypes = product.VariantTypes,
            Images = product.Images
        };

    public static Product FromProductDto(this ProductDto productDto)
        => new Product(
            productDto.Id.ToString(),
            productDto.Name,
            productDto.Headline,
            productDto.Description,
            productDto.VariantTypes,
            productDto.Images);
}
