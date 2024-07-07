namespace Company.Contract;

public record AdoptRockRequest(
    string Name,
    string Catchphrase,
    string Description,
    Dictionary<string, string> Attributes,
    List<string> Images
);
