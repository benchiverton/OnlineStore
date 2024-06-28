using Company.Contract;

namespace Company.Api.PetRocks.Dtos;

public static class PriceDtoMapper
{
    public static PriceDto ToPriceDto(this Price price)
        => new PriceDto
        {
            FullPriceGBP = price.FullPriceGBP,
            DealPriceGBP = price.DealPriceGBP,
            Details = price.Details,
        };

    public static Price FromPriceDto(this PriceDto priceDto)
        => new Price(
            priceDto.FullPriceGBP,
            priceDto.DealPriceGBP,
            priceDto.Details);
}
