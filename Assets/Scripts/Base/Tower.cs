using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : UnityEngine.MonoBehaviour
{
    //float radius;
    protected AbstractCollider myCollider;

    protected void Initialize()
    {
        InitializeCollider();
        if (myCollider != null)
        {
            myCollider.OnColliderStay += checkForAttack;
        }
    }
    void checkForAttack(AbstractCollider collider)
    {
        iAttackable enemy = collider.GetComponent<iAttackable>();
        if (enemy != null)
        {
            attackTarget(enemy);
        }
    }
    void attackTarget(iAttackable target)
    {
        target.TakeAttack(1);
    }

    protected abstract void InitializeCollider();
}
