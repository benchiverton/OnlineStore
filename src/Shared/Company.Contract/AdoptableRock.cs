namespace Company.Contract;

public record AdoptableRock(
    string Id,
    string Name,
    string Catchphrase,
    string Description,
    List<string> VariantTypes,
    List<string> Images
    );
