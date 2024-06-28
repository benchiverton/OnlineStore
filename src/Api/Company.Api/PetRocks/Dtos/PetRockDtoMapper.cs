using System;
using Company.Contract;

namespace Company.Api.PetRocks.Dtos;

public static class PetRockDtoMapper
{
    public static PetRockDto ToPetRockDto(this PetRock petRock)
        => new PetRockDto
        {
            Id = Guid.Parse(petRock.Id),
            Name = petRock.Name,
            Catchphrase = petRock.Headline,
            Description = petRock.Description,
            VariantTypes = petRock.VariantTypes,
            Images = petRock.Images
        };

    public static PetRock FromPetRockDto(this PetRockDto petRockDto)
        => new PetRock(
            petRockDto.Id.ToString(),
            petRockDto.Name,
            petRockDto.Catchphrase,
            petRockDto.Description,
            petRockDto.VariantTypes,
            petRockDto.Images);
}
