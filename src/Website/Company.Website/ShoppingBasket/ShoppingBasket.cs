using System.Collections.Generic;

namespace Company.Website.ShoppingBasket;

public class ShoppingBasket
{
    public ShoppingBasket() => PetRocks = new List<ShoppingBasketPetRock>();

    public List<ShoppingBasketPetRock> PetRocks { get; set; }
}
