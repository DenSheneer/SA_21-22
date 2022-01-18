using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUI : MonoBehaviour, System.IObserver<Enemy>
{
    iEnemyHealthGFX gfx;
    System.IDisposable cancellation;
    protected void Initialize()
    {
        gfx = defineGFX();
        Enemy enemy = fetchEnemy();
        if (enemy != null)
        {
            cancellation = enemy.Subscribe(this);
        }
    }

    protected abstract Enemy fetchEnemy();
    protected abstract iEnemyHealthGFX defineGFX();

    public void OnCompleted()
    {
        cancellation.Dispose();
    }

    public void OnError(System.Exception error)
    {
        return;
    }

    public void OnNext(Enemy enemy)
    {
        float percentage = enemy.GetHealth;
        percentage = percentage / enemy.GetMaxHealth;
        gfx.SetPercentage(percentage);
    }
}
