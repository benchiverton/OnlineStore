namespace Company.Contract;

public record Price(
    decimal FullPriceGBP,
    decimal DealPriceGBP,
    string Details
    );
