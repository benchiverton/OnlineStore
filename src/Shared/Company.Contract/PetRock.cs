namespace Company.Contract;

public record PetRock(
    string Id,
    string Name,
    string Catchphrase,
    Dictionary<string, string> Attributes,
    List<string> Images);
