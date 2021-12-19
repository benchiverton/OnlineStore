using System.Collections.Generic;

namespace Company.Website.ProductInformation;

public class ProductInformation
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description1 { get; set; }
    public string Description2 { get; set; }
    public List<string> Images { get; set; }
    public List<string> Details { get; set; }
}
