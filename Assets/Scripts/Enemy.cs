using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected string _ID;
    protected int _maxHealth;
    protected int _currentHealth;
    protected int _money;
    protected float _speed;
    protected Vector3 _currentPosition;

    public MoveAgent moveAgent;

    public static Action<Enemy> OnDeath;

    public void Move()
    {
        moveAgent.Begin();
    }
    public void Attack(iAttackable pTarget)
    {
        pTarget.TakeAttack(1);  //  TODO: how to determine damage?
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }
    public virtual void TakeAttack(int pDamage)
    {
        _currentHealth -= pDamage;
        if (_currentHealth < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        OnDeath?.Invoke(this);
    }

    public string GetID()
    {
        return _ID;
    }

}
