using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Website.ShoppingBasket;

public class ShoppingBasketService
{
    private const string ShoppingBasketStorageKey = "companyShoppingBasket";

    private readonly ISyncSessionStorageService _sessionStorage;
    private readonly List<IObserver<ShoppingBasket>> _observers;

    public ShoppingBasketService(ISyncSessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
        _observers = new List<IObserver<ShoppingBasket>>();

        var shoppingBasket = sessionStorage.GetItem<ShoppingBasket>(ShoppingBasketStorageKey);
        if (shoppingBasket == null)
        {
            shoppingBasket = new ShoppingBasket();
            sessionStorage.SetItem(ShoppingBasketStorageKey, shoppingBasket);
        }
    }

    public IDisposable Subscribe(IObserver<ShoppingBasket> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            var shoppingBasket = GetShoppingBasketFromStorage();
            observer.OnNext(shoppingBasket);
        }
        return new UnSubscriber<ShoppingBasket>(_observers, observer);
    }

    public ShoppingBasket GetShoppingBasketFromStorage() => _sessionStorage.GetItem<ShoppingBasket>(ShoppingBasketStorageKey) ?? new ShoppingBasket();

    public void AddProductVariantToBasket(string productId, string productVariantId)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingProduct = GetExistingShoppingBasketProduct(shoppingBasket, productId, productVariantId);
        if (existingProduct == null)
        {
            shoppingBasket.Products.Add(new ShoppingBasketProduct
            {
                ProductId = productId,
                VariantId = productVariantId,
                Quantity = 1
            });
        }
        else
        {
            existingProduct.Quantity++;
        }
        SetShoppingBasketInStorage(shoppingBasket);
    }

    public void UpdateQuantity(string productId, string productVariantId, int desiredQuantity)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingProduct = GetExistingShoppingBasketProduct(shoppingBasket, productId, productVariantId);
        if (existingProduct == null)
        {
            shoppingBasket.Products.Add(new ShoppingBasketProduct
            {
                ProductId = productId,
                VariantId = productVariantId,
                Quantity = desiredQuantity
            });
        }
        else if (desiredQuantity > 0)
        {
            existingProduct.Quantity = desiredQuantity;
        }
        else
        {
            shoppingBasket.Products.Remove(existingProduct);
        }
        SetShoppingBasketInStorage(shoppingBasket);
    }

    public void Delete(string productId, string productVariantId)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingProduct = GetExistingShoppingBasketProduct(shoppingBasket, productId, productVariantId);
        if (existingProduct != null)
        {
            shoppingBasket.Products.Remove(existingProduct);
            SetShoppingBasketInStorage(shoppingBasket);
        }
    }

    private ShoppingBasketProduct GetExistingShoppingBasketProduct(ShoppingBasket shoppingBasket, string productId, string productVariantId)
        => shoppingBasket.Products.FirstOrDefault(cp => cp.ProductId == productId && cp.VariantId == productVariantId);

    private void SetShoppingBasketInStorage(ShoppingBasket shoppingBasket)
    {
        _sessionStorage.SetItem(ShoppingBasketStorageKey, shoppingBasket);
        foreach (var observer in _observers)
        {
            observer.OnNext(shoppingBasket);
        }
    }
}
