using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : Unitytimer, iStatusEffect
{
    Enemy target;
    public void Initialize(Enemy target, float duration, float intensity)
    {
        base.Initialize(duration, onEnd, false);
        target.SetAgentSpeed(intensity);
        IsPaused = false;
    }
    private void onEnd()
    {
        target.RestoreSpeed();
    }
}
