using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UnityEnemyGoal : EnemyGoal
{
    [SerializeField]
    int _enemiesPresent = 0;
    private void Start()
    {
        myCollider = GetComponent<AbstractCollider>();
        base.Initialize();
    }
    protected override void updateEnemiesPresent(int newCount)
    {
        base.updateEnemiesPresent(newCount);
        _enemiesPresent = enemiesPresent;
    }
}
