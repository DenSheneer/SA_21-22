using UnityEngine;

[RequireComponent(typeof(iEnemyHealthGFX))]
public class UnityEnemyUI : EnemyUI, System.IObserver<Enemy>
{
    void Start()
    {
        Initialize();
    }
    protected override Enemy fetchEnemy()
    {
        return GetComponent<Enemy>();
    }

    protected override iEnemyHealthGFX defineGFX()
    {
        return GetComponent<iEnemyHealthGFX>();
    }
}
