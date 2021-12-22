using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityMoveAgent : MoveAgent
{
    NavMeshAgent agent;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void SetDestination(Vector3 pDestination)
    {
        agent.SetDestination(pDestination);
    }

    public override void SetPosition(Vector3 pPosition)
    {
        agent.enabled = false;
        agent.transform.position = pPosition;
        agent.enabled = true;
    }

    public override void SetMovementSpeed(float pSpeed)
    {
        agent.speed = pSpeed;
    }

    public override void ClearDestination()
    {
        agent.destination = agent.nextPosition;
    }

    public override void Begin()
    {
        agent.isStopped = false;
    }

    public override void Stop()
    {
        agent.isStopped = true;
    }

    public override Vector3 GetDestination()
    {
        return agent.destination;
    }

    public override Vector3 GetPosition()
    {
        return agent.transform.root.position;
    }

    public override float GetMoveSpeed()
    {
        return agent.speed;
    }
}
