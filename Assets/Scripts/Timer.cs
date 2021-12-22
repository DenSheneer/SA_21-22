using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [ReadOnly] [SerializeField]
    float currentTime;

    float startTime;
    bool stop = true;
    bool resetAndRepeat = false;

    public Action OnTimerEnd;

    public void Init(float pStartTime, Action pOnTimerEnd, bool pResetAndRepeat)
    {
        startTime = pStartTime;
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

    public void Pause()
    {
        stop = true;
    }

    public void Start()
    {
        stop = false;
    }

    public void Reset()
    {
        currentTime = startTime;
        stop = true;
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
                if (resetAndRepeat)
                {
                    Reset();
                    Start();
                }
            }
        }
    }
}
