using System.Collections;
using System.Collections.Generic;

public abstract class EnemyGoal : UnityEngine.MonoBehaviour
{
    protected int enemiesPresent;
    protected AbstractCollider myCollider;

    public virtual void Initialize()
    {
        if (myCollider != null)
        {
            myCollider.OnCollidersUpdate += updateEnemiesPresent;
        }
    }
    protected virtual void updateEnemiesPresent(int newCount)
    {
        enemiesPresent = newCount;
    }

}
