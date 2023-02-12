namespace Company.Contract;

public record Product(
    string Id,
    string Name,
    string Headline,
    string Description,
    List<string> VariantTypes,
    List<string> Images
    );
