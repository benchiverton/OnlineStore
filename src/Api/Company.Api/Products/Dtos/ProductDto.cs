using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Api.Products.Dtos;

public class ProductDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Headline { get; set; }
    public string Description { get; set; }
    public List<string> VariantTypes { get; set; }
    public List<string> Images { get; set; }

    public virtual ICollection<VariantDto> Variants { get; set; }
}
