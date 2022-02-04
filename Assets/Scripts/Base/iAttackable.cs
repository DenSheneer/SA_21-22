using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAttackable
{
    void TakeStatusAttack(POISON_EFFECT_STRENGTH strength);
    void RemoveStatusEffect(PoisonEffect effect);
    void TakeAttack(int pDamage);
    void Die();

    Vector3 GetPosition();
}
