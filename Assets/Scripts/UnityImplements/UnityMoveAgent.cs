using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnityMoveAgent : MonoBehaviour, iMovementController
{
    NavMeshAgent agent;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 pDestination)
    {
        agent.SetDestination(pDestination);
    }

    public void SetPosition(Vector3 pPosition)
    {
        agent.enabled = false;
        agent.transform.position = pPosition;
        agent.enabled = true;
    }

    public void SetMovementSpeed(float pSpeed)
    {
        agent.speed = pSpeed;
    }

    public void ClearDestination()
    {
        agent.destination = agent.nextPosition;
    }

    public void Begin()
    {
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    public Vector3 GetDestination()
    {
        return agent.destination;
    }

    public Vector3 GetPosition()
    {
        return agent.transform.root.position;
    }

    public float GetMoveSpeed()
    {
        return agent.speed;
    }
    public float GetCurrentSpeed()
    {
        return agent.velocity.magnitude;
    }
    public bool IsMoving()
    {
        if (agent.velocity != Vector3.zero)
            return true;
        else
            return false;
    }
}
