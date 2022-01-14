using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy abstract base class. Inherits from Monobehaviour for Unity compatibility.
/// </summary>
public abstract class Enemy : UnityEngine.MonoBehaviour, iAttackable, System.IObservable<Enemy>
{
    protected string _ID;
    protected int _maxHealth;
    protected int _currentHealth;
    protected int _money;
    protected List<IObserver<Enemy>> observers;

    protected iMovementController moveAgent;
    protected AbstractCollider myCollider;

    public Action<Enemy> OnDeath;

    /// <summary>
    /// Constructor of the Enemy class. (Is called 'Initialize' for compatibility with Unity)
    /// </summary>
    public virtual void Initialize(string pID, int pMaxHealth, int pMoney, float pSpeed, Vector3 pSpawn)
    {
        _ID = pID;
        _maxHealth = pMaxHealth;
        _currentHealth = pMaxHealth;
        _money = pMoney;
        observers = new List<IObserver<Enemy>>();
        setupMoveAgent(pSpawn, pSpeed);
    }
    public void Attack(iAttackable pTarget)
    {
        pTarget.TakeAttack(1);  //  TODO: how to determine damage?
    }

    public virtual void TakeAttack(int pDamage)
    {
        _currentHealth -= pDamage;
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        OnDeath?.Invoke(this);

        foreach(var observer in observers)
        {
            observer.OnCompleted();
        }
    }

    public string GetID()
    {
        return _ID;
    }
    public int GetHealth { get { return _currentHealth; } }
    public int GetMaxHealth { get { return _maxHealth; } }
    /// <summary>
    /// Method for defining the moveAgent. Implement in concrete class.
    /// </summary>
    protected abstract void setupMoveAgent(Vector3 pSpawn, float pSpeed);

    public IDisposable Subscribe(System.IObserver<Enemy> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<Enemy>(observers, observer);
    }

    internal class Unsubscriber<Enemy> : IDisposable
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
}
