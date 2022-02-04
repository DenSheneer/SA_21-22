using System.Collections;
using System.Collections.Generic;

public interface iAttackable
{
    void TakeStatusAttack(POISON_EFFECT_STRENGTH strength);
    void RemoveStatusEffect(PoisonEffect effect);
    void TakeAttack(int pDamage);
    void Die();
}
