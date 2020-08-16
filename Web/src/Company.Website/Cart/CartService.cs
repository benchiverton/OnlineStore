using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Website.Cart
{
    public class CartService
    {
        private const string CartKey = "myShoppingCart";

        private readonly ISyncSessionStorageService _sessionStorage;
        private readonly List<IObserver<Cart>> _observers;

        public CartService(ISyncSessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
            _observers = new List<IObserver<Cart>>();

            var shoppingCart = sessionStorage.GetItem<Cart>(CartKey);
            if (shoppingCart == null)
            {
                shoppingCart = new Cart();
                sessionStorage.SetItem(CartKey, shoppingCart);
            }
        }

        public IDisposable Subscribe(IObserver<Cart> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                var cart = GetCartFromStorage();
                observer.OnNext(cart);
            }
            return new UnSubscriber<Cart>(_observers, observer);
        }

        public Cart GetCartFromStorage() => _sessionStorage.GetItem<Cart>(CartKey) ?? new Cart();

        public void AddProductVariantToCart(int productVariantId)
        {
            var cart = GetCartFromStorage();
            var existingProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductVariantId == productVariantId);
            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                cart.CartProducts.Add(new CartProduct
                {
                    ProductVariantId = productVariantId,
                    Quantity = 1
                });
            }
            SetCartInStorage(cart);
        }

        private void SetCartInStorage(Cart cart)
        {
            _sessionStorage.SetItem(CartKey, cart);
            foreach (var observer in _observers)
            {
                observer.OnNext(cart);
            }
        }
    }
}
