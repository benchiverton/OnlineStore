namespace Company.Contract;

public record AdoptableRock(
    string Id,
    string Name,
    string Catchphrase,
    string Description,
    Dictionary<string, List<string>> CustomisableAttributes,
    List<string> Images
    );
