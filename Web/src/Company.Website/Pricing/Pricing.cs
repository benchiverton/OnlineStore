using System;

namespace Company.Website.Pricing
{
    public class Pricing
    {
        public int Id { get; set; }
        public decimal FullPriceGBP { get; set; }
        // abstract deal logic out to a deal service or something
        public decimal DealPriceGBP { get; set; }
        public string Details { get; set; }
    }
}
