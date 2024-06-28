namespace Company.Contract;

public record PetRock(
    string Id,
    string Name,
    string Headline,
    string Description,
    List<string> VariantTypes,
    List<string> Images
    );
