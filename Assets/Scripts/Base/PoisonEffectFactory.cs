using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffectFactory
{
    public PoisonEffectProperties Produce(POISON_EFFECT_STRENGTH strength)
    {
        PoisonEffectProperties poisonEffect = new PoisonEffectProperties();
        switch (strength)
        {
            case POISON_EFFECT_STRENGTH.strong:
                poisonEffect.damage = 50;
                poisonEffect.totalTicks = 10;
                poisonEffect.tickTime = 1.0f;
                break;
            case POISON_EFFECT_STRENGTH.weak:
                poisonEffect.damage = 25;
                poisonEffect.totalTicks = 10;
                poisonEffect.tickTime = 1.0f;
                break;
        }
        return poisonEffect;
    }
}
public enum POISON_EFFECT_STRENGTH
{
    weak,
    strong
}
