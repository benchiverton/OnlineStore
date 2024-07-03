using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Api.Adoption.Dtos
{
    public class PetRockDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Name { get; set; }
        public string Catchphrase { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> Images { get; set; }
    }
}
