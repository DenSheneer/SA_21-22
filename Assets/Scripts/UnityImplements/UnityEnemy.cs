using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(iMovementController))]
public class UnityEnemy : Enemy
{
    Animator anim;
    public override void Initialize(string pID, int pMaxHealth, uint pMoney, float pSpeed, Vector3 pSpawn, Vector3 pDestination)
    {
        base.Initialize(pID, pMaxHealth, pMoney, pSpeed, pSpawn, pDestination);
        gameObject.name = pID;

        anim = GetComponent<Animator>();
        myCollider = GetComponent<AbstractCollider>();
    }
    public void Initialize(EnemyProperties properties, Vector3 pDestination)
    {
        Initialize(properties.typename, properties.maxHealth, properties.money, properties.moveSpeed, properties.spawnLocation, pDestination);
    }

    public void Update()
    {
        if (moveAgent.IsMoving())
        {
            anim.SetFloat("Speed", moveAgent.GetCurrentSpeed() / moveAgent.GetMoveSpeed());
        }
    }

    protected override void setupMoveAgent(Vector3 pSpawn, float pSpeed, Vector3 destination)
    {
        moveAgent = GetComponent<iMovementController>();
        moveAgent.SetPosition(pSpawn);
        moveAgent.SetMovementSpeed(pSpeed);
        moveAgent.SetDestination(destination);
    }

    protected override void deleteSelf()
    {
        foreach (var observer in observers.ToArray())
            if (observers.Contains(observer))
                observer.OnCompleted();

        Destroy(gameObject);
    }
}
