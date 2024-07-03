using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Api.PetRocks.Dtos;

public class VariantDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid PetRockId { get; set; }
    [ForeignKey("PetRockId")]
    public PetRockDto PetRock { get; set; }
    public Dictionary<string, string> VariantTypeValues { get; set; }
}
