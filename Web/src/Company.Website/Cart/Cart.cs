using System.Collections.Generic;

namespace Company.Website.Cart;

public class Cart
{
    public Cart() => CartProducts = new List<CartProduct>();

    public List<CartProduct> CartProducts { get; set; }
}
