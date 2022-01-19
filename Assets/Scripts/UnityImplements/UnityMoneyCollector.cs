using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMoneyCollector : MoneyCollector
{
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
    void Start()
    {
        EnemySpawnHandler.Instance.OnEnemySpawn += AddMoneyFromEnemy;
    }
}
