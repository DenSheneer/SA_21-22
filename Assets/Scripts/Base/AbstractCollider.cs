using System.Collections.Generic;
using System;

public abstract class AbstractCollider : UnityEngine.MonoBehaviour
{
    protected List<AbstractCollider> colliders = new List<AbstractCollider>();
    public Action<int> OnCollidersUpdate;
    public Action<AbstractCollider> OnColliderStay;
    protected virtual void OnTriggerEnter(AbstractCollider other)
    {
        if (!colliders.Contains(other))
        {
            colliders.Add(other);
            OnCollidersUpdate?.Invoke(GetColliderCount);
        }
    }
    protected virtual void OnTriggerExit(AbstractCollider other)
    {
        if (colliders.Contains(other))
        {
            colliders.Remove(other);
            OnCollidersUpdate?.Invoke(GetColliderCount);
        }
    }
    protected virtual void OnTrigStay(AbstractCollider other)
    {
        OnColliderStay?.Invoke(other);
    }
    public int GetColliderCount { get { return colliders.Count; } }
}
