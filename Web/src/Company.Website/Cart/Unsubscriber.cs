using System;
using System.Collections.Generic;

namespace Company.Website.Cart
{
    internal class Unsubscriber<Cart> : IDisposable
    {
        private readonly List<IObserver<Cart>> _observers;
        private readonly IObserver<Cart> _observer;

        internal Unsubscriber(List<IObserver<Cart>> observers, IObserver<Cart> observer)
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
}
