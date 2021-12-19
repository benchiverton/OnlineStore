using System;
using System.Linq;

namespace Company.Website.Cart;

public class CartMonitor : IObserver<Cart>
{
    private IDisposable _cancellation;
    private Action _updateAction;

    public int CartSize { get; private set; }

    public virtual void Subscribe(CartService provider, Action onNext = null)
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

    public void OnNext(Cart value)
    {
        CartSize = value.CartProducts.Sum(cp => cp.Quantity);
        _updateAction?.Invoke();
    }
}
