using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoneyCollector : MonoBehaviour
{
    [SerializeField]
    uint money = 0;

    protected static MoneyCollector _instance;
    public static MoneyCollector Instance { get { return _instance; } }

    public void AddMoney(uint amount) { money += amount; }
    public bool RemoveMoney(uint amount)
    {
        if (amount >= money) { money -= amount; return true; }
        else { return false; }
    }
    public void AddMoneyFromEnemy(Enemy enemy)
    {
        AddMoney(enemy.GetMoney);
    }
}
