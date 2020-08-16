using System;
using System.Linq;

namespace Company.Website.Cart
{
    public class CartMonitor : IObserver<Cart>
    {
        private IDisposable cancellation;
        private Action updateAction;

        public int CartSize;

        public virtual void Subscribe(CartService provider, Action onNext = null)
        {
            updateAction = onNext;
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }

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
            updateAction?.Invoke();
        }
    }
}
