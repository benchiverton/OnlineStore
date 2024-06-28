
namespace Company.Contract;

public record Variant(
    string PetRockId,
    string VariantId,
    Dictionary<string, string> VariantTypeValues,
    Price Price
    );
