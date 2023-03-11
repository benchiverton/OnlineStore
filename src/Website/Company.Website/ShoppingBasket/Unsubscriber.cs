using System;
using System.Collections.Generic;

namespace Company.Website.ShoppingBasket;

internal class UnSubscriber<TShoppingBasket> : IDisposable where TShoppingBasket : ShoppingBasket
{
    private readonly List<IObserver<TShoppingBasket>> _observers;
    private readonly IObserver<TShoppingBasket> _observer;

    internal UnSubscriber(List<IObserver<TShoppingBasket>> observers, IObserver<TShoppingBasket> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}
