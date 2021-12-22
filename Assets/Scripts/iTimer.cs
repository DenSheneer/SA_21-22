using System.Collections;
using System.Collections.Generic;
using System;

public interface iTimer
{
    void SetTime(float pTimeLeft);
    float GetTimeLeft();
    void Pause();
    void Start();
    void Reset();
    void SetResetAndRepeat(bool mode);
    void Init(float pStartTime, Action pOnTimerEnd, bool pResetAndRepeat);
}
