using System;
using System.Linq;

namespace Company.Website.ShoppingBasket;

public class ShoppingBasketMonitor : IObserver<ShoppingBasket>
{
    private IDisposable _cancellation;
    private Action _updateAction;

    public int ShoppingBasketSize { get; private set; }

    public virtual void Subscribe(ShoppingBasketService provider, Action onNext = null)
    {
        _updateAction = onNext;
        _cancellation = provider.Subscribe(this);
    }

    public virtual void Unsubscribe() => _cancellation.Dispose();

    public void OnCompleted()
    {
        // do nothing
    }

    public void OnError(Exception error)
    {
        // do nothing
    }

    public void OnNext(ShoppingBasket value)
    {
        ShoppingBasketSize = value.Products.Sum(cp => cp.Quantity);
        _updateAction?.Invoke();
    }
}
