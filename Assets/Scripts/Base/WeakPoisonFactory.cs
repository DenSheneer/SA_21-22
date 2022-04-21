using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeakPoisonFactory : PoisonEffectFactory
{
    public override PoisonEffectProperties Produce()
    {
        PoisonEffectProperties poisonEffect = new PoisonEffectProperties();
        poisonEffect.damage = 25;
        poisonEffect.totalTicks = 10;
        poisonEffect.tickTime = 1.0f;
        return poisonEffect;
    }
}
