using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    [SerializeField]
    private CustomUnityEvent onDeath;

    int carriedMoney = 69;

    private void Awake()
    {
        onDeath?.Invoke(carriedMoney);
    }
}
