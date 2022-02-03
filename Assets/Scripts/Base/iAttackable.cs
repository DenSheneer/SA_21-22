using System.Collections;
using System.Collections.Generic;

public interface iAttackable
{
    void TakeStatusAttack(float tickTime, int ticks, int damage);
    void RemoveStatusEffect(PoisonEffect effect);
    void TakeAttack(int pDamage);
    void Die();
}
