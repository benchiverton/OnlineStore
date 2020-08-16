using System;
using System.Collections.Generic;

namespace Company.Website.Cart
{
    internal class UnSubscriber<TCart> : IDisposable where TCart : Cart
    {
        private readonly List<IObserver<TCart>> _observers;
        private readonly IObserver<TCart> _observer;

        internal UnSubscriber(List<IObserver<TCart>> observers, IObserver<TCart> observer)
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
