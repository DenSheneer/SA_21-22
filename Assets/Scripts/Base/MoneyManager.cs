using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    uint money = 0;

    public System.Action<uint> OnMoneyChange;
    public System.Action<uint> OnMoneyAdd;

    public void AddMoney(uint amount) { money += amount; OnMoneyChange?.Invoke(money); OnMoneyAdd?.Invoke(amount); }

    public void SetMoney(uint money) { this.money = money; OnMoneyChange?.Invoke(money); }
    public uint Money { get { return money; } }
    public bool RemoveMoney(uint amount)
    {

        if (amount <= money) { money -= amount; OnMoneyChange?.Invoke(money);  return true; }
        else { return false; }
    }
}
