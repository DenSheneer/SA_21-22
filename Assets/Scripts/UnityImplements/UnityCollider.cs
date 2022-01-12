using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Collider))]
[RequireComponent(typeof(UnityEngine.Rigidbody))]
public class UnityCollider : AbstractCollider
{
    [SerializeField]
    protected UnityEngine.Collider unityCollider;
    [SerializeField]
    protected UnityEngine.Rigidbody rb;
    private void Start()
    {
        unityCollider = GetComponent<UnityEngine.Collider>();
        unityCollider.isTrigger = true;

        rb = GetComponent<UnityEngine.Rigidbody>();
        rb.isKinematic = true;
    }


    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        AbstractCollider myCollider = other.GetComponent<AbstractCollider>();
        if (myCollider != null)
        {
            base.OnTriggerEnter(myCollider);
        }
    }
    private void OnTriggerExit(UnityEngine.Collider other)
    {
        AbstractCollider myCollider = other.GetComponent<AbstractCollider>();
        if (myCollider != null)
        {
            base.OnTriggerExit(myCollider);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        AbstractCollider myCollider = other.GetComponent<AbstractCollider>();
        if (myCollider != null)
        {
            base.OnTrigStay(myCollider);
        }
    }
}
