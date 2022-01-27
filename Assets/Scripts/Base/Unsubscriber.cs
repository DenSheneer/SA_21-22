using System.Collections;
using System.Collections.Generic;
using System;

public class Unsubscriber<Enemy> : IDisposable
{
    private List<IObserver<Enemy>> _observers;
    private IObserver<Enemy> _observer;

    internal Unsubscriber(List<IObserver<Enemy>> observers, IObserver<Enemy> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}