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

    public void AddPetRockVariantToBasket(string petRockId, string petRockVariantId)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingPetRock = GetExistingShoppingBasketPetRock(shoppingBasket, petRockId, petRockVariantId);
        if (existingPetRock == null)
        {
            shoppingBasket.PetRocks.Add(new ShoppingBasketPetRock
            {
                PetRockId = petRockId,
                VariantId = petRockVariantId,
                Quantity = 1
            });
        }
        else
        {
            existingPetRock.Quantity++;
        }
        SetShoppingBasketInStorage(shoppingBasket);
    }

    public void UpdateQuantity(string petRockId, string petRockVariantId, int desiredQuantity)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingPetRock = GetExistingShoppingBasketPetRock(shoppingBasket, petRockId, petRockVariantId);
        if (existingPetRock == null)
        {
            shoppingBasket.PetRocks.Add(new ShoppingBasketPetRock
            {
                PetRockId = petRockId,
                VariantId = petRockVariantId,
                Quantity = desiredQuantity
            });
        }
        else if (desiredQuantity > 0)
        {
            existingPetRock.Quantity = desiredQuantity;
        }
        else
        {
            shoppingBasket.PetRocks.Remove(existingPetRock);
        }
        SetShoppingBasketInStorage(shoppingBasket);
    }

    public void Delete(string petRockId, string petRockVariantId)
    {
        var shoppingBasket = GetShoppingBasketFromStorage();
        var existingPetRock = GetExistingShoppingBasketPetRock(shoppingBasket, petRockId, petRockVariantId);
        if (existingPetRock != null)
        {
            shoppingBasket.PetRocks.Remove(existingPetRock);
            SetShoppingBasketInStorage(shoppingBasket);
        }
    }

    private ShoppingBasketPetRock GetExistingShoppingBasketPetRock(ShoppingBasket shoppingBasket, string petRockId, string petRockVariantId)
        => shoppingBasket.PetRocks.FirstOrDefault(cp => cp.PetRockId == petRockId && cp.VariantId == petRockVariantId);

    private void SetShoppingBasketInStorage(ShoppingBasket shoppingBasket)
    {
        _sessionStorage.SetItem(ShoppingBasketStorageKey, shoppingBasket);
        foreach (var observer in _observers)
        {
            observer.OnNext(shoppingBasket);
        }
    }
}
