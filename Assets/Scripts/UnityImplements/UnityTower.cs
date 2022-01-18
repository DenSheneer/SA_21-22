using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTower : Tower
{
    [SerializeField]
    TowerProperties towerProperties;

    public void Initialize(TowerProperties pTowerProperties)
    {
        base.Initialize(pTowerProperties.AttackMode, pTowerProperties.Damage, pTowerProperties.AttackTick, pTowerProperties.AttackRadius);
    }

    void Start()
    {
        Initialize(towerProperties);
    }

    protected override Timer defineTickTimer()
    {
        return GetComponent<Timer>();
    }
}
