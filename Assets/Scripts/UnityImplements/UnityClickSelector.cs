using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UnityClickSelector : MonoBehaviour, iTowerSelector
{
    Tower currentSelectedTower;
    private List<IObserver<Tower>> observers = new List<IObserver<Tower>>();
    public Tower GetSelectedTower()
    {
        return currentSelectedTower;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
    public void OnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hit.transform.TryGetComponent(out Tower nextSelect);
            if (nextSelect != null)
            {
                currentSelectedTower = nextSelect;
                foreach (var observer in observers)
                {
                    observer.OnNext(nextSelect);
                }
            }
        }
    }

    public IDisposable Subscribe(System.IObserver<Tower> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<Tower>(observers, observer);
    }
}
