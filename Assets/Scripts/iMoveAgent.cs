using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public interface iMoveAgent
{
    void SetDestination(Vector3 pDestination);
    Vector3 GetDestination();
    void SetMovementSpeed(float pSpeed);
    void ClearDestination();
    void Start();
    void Stop();

}
