namespace Company.Contract;

public record Price(
    int Id,
    decimal FullPriceGBP,
    decimal DealPriceGBP,
    string Details
    );
