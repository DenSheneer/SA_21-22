using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityCollider))]
[RequireComponent(typeof(Rigidbody))]
public class UnityTower : Tower
{
    Rigidbody rb;

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

    protected override void initializeCollider()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    protected override Timer defineTickTimer()
    {
        return GetComponent<Timer>();
    }
}
