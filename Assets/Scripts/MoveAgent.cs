using UnityEngine;
public abstract class MoveAgent : MonoBehaviour
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

}
