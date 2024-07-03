using System;
using Company.Contract;

namespace Company.Api.Adoption.Dtos
{
    public static class AdoptableRockVariantDtoMapper
    {
        public static AdoptableRockVariantDto ToVariantDto(this AdoptableRockVariant adoptableRockVariant)
            => new AdoptableRockVariantDto
            {
                Id = Guid.Parse(adoptableRockVariant.VariantId),
                PetRockId = Guid.Parse(adoptableRockVariant.PetRockId),
                VariantTypeValues = adoptableRockVariant.VariantTypeValues
            };

        public static AdoptableRockVariant FromVariantDto(this AdoptableRockVariantDto adoptableRockVariantDto)
            => new AdoptableRockVariant(
                adoptableRockVariantDto.PetRockId.ToString(),
                adoptableRockVariantDto.Id.ToString(),
                adoptableRockVariantDto.VariantTypeValues);
    }
}
