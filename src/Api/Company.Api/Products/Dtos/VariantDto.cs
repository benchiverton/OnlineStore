using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Products.Dtos;

public class VariantDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public ProductDto Product { get; set; }
    public Dictionary<string, string> VariantTypeValues { get; set; }
    public PriceDto Price { get; set; }
}
