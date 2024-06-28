using System;

namespace Company.Api.PetRocks.Dtos;

public class PriceDto
{
    public Guid VariantDtoId { get; set; }
    public decimal FullPriceGBP { get; set; }
    public decimal DealPriceGBP { get; set; }
    public string Details { get; set; }
}
