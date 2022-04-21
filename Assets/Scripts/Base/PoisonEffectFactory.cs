using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public abstract class PoisonEffectFactory : ScriptableObject
{
    public abstract PoisonEffectProperties Produce();
}
