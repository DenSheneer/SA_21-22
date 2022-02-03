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
    protected uint _money;
    protected List<IObserver<Enemy>> observers;

    protected iMovementController moveAgent;
    protected AbstractCollider myCollider;

    public Action<Enemy> OnDeath;

    /// <summary>
    /// Constructor of the Enemy class. (Is called 'Initialize' for compatibility with Unity)
    /// </summary>
    public virtual void Initialize(string pID, int pMaxHealth, uint pMoney, float pSpeed, Vector3 pSpawn)
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

        //Debug.Log("damaged: " + _currentHealth + " listeners: " + observers.Count);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        OnDeath?.Invoke(this);

        deleteSelf();
    }
    public void GoalReached()
    {
        deleteSelf();
    }

    public void Delete()
    {
        deleteSelf();
    }

    public string GetID { get { return _ID; } }
    public uint GetMoney { get { return _money; } }
    public int GetHealth { get { return _currentHealth; } }
    public int GetMaxHealth { get { return _maxHealth; } }
    /// <summary>
    /// Method for defining the moveAgent. Implement in concrete class.
    /// </summary>
    protected abstract void setupMoveAgent(Vector3 pSpawn, float pSpeed);
    protected abstract void deleteSelf();

    public IDisposable Subscribe(System.IObserver<Enemy> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<Enemy>(observers, observer);
    }
}
