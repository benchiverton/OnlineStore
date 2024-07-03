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
                AdoptableRockId = Guid.Parse(adoptableRockVariant.PetRockId),
                VariantTypeValues = adoptableRockVariant.VariantTypeValues
            };

        public static AdoptableRockVariant FromVariantDto(this AdoptableRockVariantDto adoptableRockVariantDto)
            => new AdoptableRockVariant(
                adoptableRockVariantDto.AdoptableRockId.ToString(),
                adoptableRockVariantDto.Id.ToString(),
                adoptableRockVariantDto.VariantTypeValues);
    }
}
