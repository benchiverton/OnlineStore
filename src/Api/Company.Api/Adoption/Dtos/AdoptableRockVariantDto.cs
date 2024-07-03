using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Api.Adoption.Dtos
{
    public class AdoptableRockVariantDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid PetRockId { get; set; }
        [ForeignKey("PetRockId")]
        public AdoptableRockDto AdoptableRock { get; set; }
        public Dictionary<string, string> VariantTypeValues { get; set; }
    }
}
