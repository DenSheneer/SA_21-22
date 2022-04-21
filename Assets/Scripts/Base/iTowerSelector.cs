using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface iTowerSelector : IObservable<Tower>
{
    Tower GetSelectedTower();
    void OnClick();
}
