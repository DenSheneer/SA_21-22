using System;
using UnityEngine;
using UnityEditor;

public class UnityTimer : MonoBehaviour, iTimer
{
    ConcreteTimer timer = new ConcreteTimer();

    [ReadOnly]
    [SerializeField]
    private float currentTime;

    public void Update()
    {
        timer.Update(Time.deltaTime);
        currentTime = timer.GetTimeLeft();
    }

    public void SetTime(float pTimeLeft) { timer.SetTime(pTimeLeft); }

    public float GetTimeLeft() { return timer.GetTimeLeft(); }

    public void Pause() { timer.Pause(); }

    public void Start() { timer.Start(); }

    public void Reset() { timer.Reset(); }

    public void SetResetAndRepeat(bool mode) { timer.SetResetAndRepeat(mode); }

    public void Init(float pStartTime, Action pOnTimerEnd, bool pResetAndRepeat)
    {
        timer.Init(pStartTime, pOnTimerEnd, pResetAndRepeat);
    }
}
