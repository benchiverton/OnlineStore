
namespace Company.Contract;

public record Variant(
    string ProductId,
    string VariantId,
    Dictionary<string, string> VariantTypeValues,
    Price Price
    );
