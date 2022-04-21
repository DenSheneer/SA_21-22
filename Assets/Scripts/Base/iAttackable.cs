using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAttackable
{
    void TakeStatusAttack(PoisonEffectFactory poisonEffect);
    void RemoveCurrentStatusEffect();
    void TakeAttack(int pDamage);
    void Die();

    Vector3 GetPosition();
}
