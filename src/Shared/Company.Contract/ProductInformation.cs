namespace Company.Contract;

public record ProductInformation(
    string Id,
    string Name,
    string Description1,
    string Description2,
    List<string> Images,
    List<string> Details
    );
