using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Unitytimer, iStatusEffect
{
    int totalTicks;
    int currentTick = 0;
    int damage;
    iAttackable target;
    public void Initialize(iAttackable target, float tickTime, int ticks, int damage)
    {
        this.totalTicks = ticks;
        this.target = target;
        this.damage = damage;
        base.Initialize(tickTime, onTimerEnd, true);
        IsPaused = false;
    }
    void onTimerEnd()
    {
        target.TakeAttack(damage);
        currentTick++;
        if (currentTick >= totalTicks)
        {
            target.RemoveStatusEffect(this);
        }
    }
}
