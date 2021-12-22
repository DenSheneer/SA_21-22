using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public abstract class Enemy : iAttackable
{
    private string ID;
    private int maxHealth;
    private int currentHealth;
    private int money;

    iMoveAgent moveAgent;

    public static Action<Enemy> OnDeath;

    public Enemy(string pID, int pMaxHealth, int pMoney, float pSpeed, Vector3 pSpawn)
    {
        ID = pID;
        maxHealth = pMaxHealth;
        currentHealth = pMaxHealth;
        money = pMoney;
        //moveAgent.SetMovementSpeed(pSpeed);
    }
    public Enemy(Enemy enemy)
    {
        ID = enemy.ID;
        maxHealth = enemy.maxHealth;
        currentHealth = enemy.currentHealth;
        money = enemy.money;
        moveAgent = enemy.moveAgent;
    }

    public void SetTargetDestination(Vector3 pDesination)
    {
        moveAgent.SetDestination(pDesination);
    }
    public void Move()
    {
        moveAgent.Start();
    }
    public void Attack(iAttackable pTarget)
    {
        pTarget.TakeAttack(1);  //  TODO: how to determine damage?
    }

    public void TakeAttack(int pDamage)
    {
        currentHealth -= pDamage;
        if (currentHealth < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        OnDeath?.Invoke(this);
    }
}
