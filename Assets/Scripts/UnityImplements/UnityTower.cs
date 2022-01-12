using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityCollider))]
[RequireComponent(typeof(Rigidbody))]
public class UnityTower : Tower
{
    Rigidbody rb;

    void Start()
    {
        Initialize();
    }

    protected override void InitializeCollider()
    {
        myCollider = GetComponent<UnityCollider>();
        rb = GetComponent<Rigidbody>();
    }
}
