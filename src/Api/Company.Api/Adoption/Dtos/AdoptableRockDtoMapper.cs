﻿using System;
using Company.Contract;

namespace Company.Api.Adoption.Dtos
{
    public static class AdoptableRockDtoMapper
    {
        public static AdoptableRockDto ToPetRockDto(this AdoptableRock adoptableRock)
            => new AdoptableRockDto
            {
                Id = Guid.Parse(adoptableRock.Id),
                Name = adoptableRock.Name,
                Catchphrase = adoptableRock.Catchphrase,
                Description = adoptableRock.Description,
                CustomisableAttributes = adoptableRock.CustomisableAttributes,
                Images = adoptableRock.Images
            };

        public static AdoptableRock FromPetRockDto(this AdoptableRockDto adoptableRockDto)
            => new AdoptableRock(
                adoptableRockDto.Id.ToString(),
                adoptableRockDto.Name,
                adoptableRockDto.Catchphrase,
                adoptableRockDto.Description,
                adoptableRockDto.CustomisableAttributes,
                adoptableRockDto.Images);
    }
}
