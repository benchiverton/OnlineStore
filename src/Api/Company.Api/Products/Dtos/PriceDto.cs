using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Api.Products.Dtos;

public class PriceDto
{
    public Guid VariantDtoId { get; set; }
    public decimal FullPriceGBP { get; set; }
    public decimal DealPriceGBP { get; set; }
    public string Details { get; set; }
}
