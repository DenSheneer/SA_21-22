using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Unitytimer
{
    int totalTicks;
    int currentTick = 0;
    int damage;
    iAttackable target;
    public void Initialize(iAttackable target, PoisonEffectProperties properties)
    {
        this.target = target;
        totalTicks = properties.totalTicks;
        damage = properties.damage;
        base.Initialize(properties.tickTime, onTimerEnd, true);
        IsPaused = false;
        onTimerEnd();
    }


    void onTimerEnd()
    {
        target.TakeAttack(damage);
        currentTick++;
        if (currentTick >= totalTicks)
        {
            target.RemoveCurrentStatusEffect();
        }
    }
}
