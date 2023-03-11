using System.Collections.Generic;

namespace Company.Website.ShoppingBasket;

public class ShoppingBasket
{
    public ShoppingBasket() => Products = new List<ShoppingBasketProduct>();

    public List<ShoppingBasketProduct> Products { get; set; }
}
