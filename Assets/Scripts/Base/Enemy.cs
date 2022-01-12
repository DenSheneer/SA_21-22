using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy abstract base class. Inherits from Monobehaviour for Unity compatibility.
/// </summary>
public abstract class Enemy : UnityEngine.MonoBehaviour, iAttackable
{
    protected string _ID;
    protected int _maxHealth;
    protected int _currentHealth;
    protected int _money;

    public iMovementController moveAgent;
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
        setupMoveAgent(pSpawn, pSpeed);
    }
    public void Attack(iAttackable pTarget)
    {
        pTarget.TakeAttack(1);  //  TODO: how to determine damage?
    }

    public virtual void TakeAttack(int pDamage)
    {
        _currentHealth -= pDamage;
        Debug.Log(_currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        OnDeath?.Invoke(this);
    }

    public string GetID()
    {
        return _ID;
    }
    /// <summary>
    /// Method for defining the moveAgent. Implement in concrete class.
    /// </summary>
    protected abstract void setupMoveAgent(Vector3 pSpawn, float pSpeed);
}
