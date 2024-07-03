
namespace Company.Contract;

public record AdoptableRockVariant(
    string PetRockId,
    string VariantId,
    Dictionary<string, string> VariantTypeValues
    );
