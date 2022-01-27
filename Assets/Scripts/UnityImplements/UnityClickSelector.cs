using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UnityClickSelector : MonoBehaviour, iTowerSelector
{
    Transform selected;
    private List<IObserver<Transform>> observers = new List<IObserver<Transform>>();
    public Transform GetSelectedSpace()
    {
        return selected;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerClick();
        }
    }
    public void OnPointerClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "tower")
            {
                selected = objectHit;
                foreach (var observer in observers)
                {
                    observer.OnNext(selected);
                }
            }
        }
    }

    public void OnSpaceClick()
    {
        
    }

    public IDisposable Subscribe(System.IObserver<Transform> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<Transform>(observers, observer);
    }
}
