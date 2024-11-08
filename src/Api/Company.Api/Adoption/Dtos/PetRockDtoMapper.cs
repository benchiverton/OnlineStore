using Company.Contract;

namespace Company.Api.Adoption.Dtos
{
    public static class PetRockDtoMapper
    {
        public static PetRock FromPetRockDto(this PetRockDto adoptableRockDto)
            => new PetRock(
                adoptableRockDto.Id.ToString(),
                adoptableRockDto.Name,
                adoptableRockDto.Catchphrase,
                adoptableRockDto.Attributes,
                adoptableRockDto.Images);
    }
}
