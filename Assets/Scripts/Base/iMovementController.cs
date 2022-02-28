using UnityEngine;

/// <summary>
/// Interface for implementation of movement methods.
/// </summary>
public interface iMovementController
{
    public abstract void SetDestination(Vector3 pDestination);
    public abstract void SetPosition(Vector3 pPosition);
    public abstract void SetMovementSpeed(float pSpeed);
    public abstract void ClearDestination();
    public abstract void Begin();
    public abstract void Stop();
    public abstract Vector3 GetDestination();
    public abstract Vector3 GetPosition();
    public abstract float GetMoveSpeed();
    public abstract float GetCurrentSpeed();
    public abstract bool IsMoving();
}
