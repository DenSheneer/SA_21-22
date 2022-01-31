using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoneyManager : MonoBehaviour
{
    [SerializeField]
    uint money = 0;
    [SerializeField]
    TowerUpgradeCosts costs;

    protected static MoneyManager _instance;
    public static MoneyManager Instance { get { return _instance; } }

    public void AddMoney(uint amount) { money += amount; }

    public void SetMoney(uint money) { this.money = money; }
    public uint Money { get { return money; } }
    public bool RemoveMoney(uint amount)
    {

        if (amount <= money) { money -= amount; return true; }
        else { return false; }
    }
    public void AddMoneyFromEnemy(Enemy enemy)
    {
        AddMoney(enemy.GetMoney);
        enemy.OnDeath -= AddMoneyFromEnemy;
    }
    public uint GetUpgradeCosts(TOWER_TIER currentTier)
    {
        if (currentTier != TOWER_TIER.strong)
        {
            return costs.GetCosts[(int)currentTier + 1];
        }
        else { return uint.MaxValue; }

    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
