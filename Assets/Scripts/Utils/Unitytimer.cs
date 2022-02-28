using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unitytimer : MonoBehaviour
{
    [SerializeField]
    float currentTime;

    float startTime;
    bool stop = true;
    bool resetAndRepeat = false;
    public Action OnTimerEnd;
    public virtual void Initialize(float pStartTime, Action pOnTimerEnd, bool pResetAndRepeat)
    {
        SetTime(pStartTime);
        OnTimerEnd += pOnTimerEnd;
        resetAndRepeat = pResetAndRepeat;
    }
    public void SetTime(float pTimeLeft)
    {
        startTime = pTimeLeft;
        currentTime = pTimeLeft;
    }
    public float GetTimeLeft()
    {
        return currentTime;
    }
    public bool IsPaused { get { return stop; } set { stop = value; } }
    public void ResetTimer()
    {
        currentTime = startTime;
        if (resetAndRepeat) { IsPaused = false; }
        else { IsPaused = true; }
    }
    public void SetResetAndRepeat(bool mode)
    {
        resetAndRepeat = mode;
    }
    public void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!stop)
        {
            currentTime -= deltaTime;
            if (currentTime <= 0)
            {
                OnTimerEnd?.Invoke();
                ResetTimer();
            }
        }
    }
}
