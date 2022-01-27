using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTower : Tower
{
    protected override Timer defineTickTimer()
    {
        return GetComponent<Timer>();
    }

    protected override iTowerGraphicsBuilder defineGraphicsBuilder()
    {
        return GetComponent<iTowerGraphicsBuilder>();
    }
}
