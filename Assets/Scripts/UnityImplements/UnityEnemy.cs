using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(iMovementController))]
public class UnityEnemy : Enemy
{
    Animator anim;
    public override void Initialize(string pID, int pMaxHealth, uint pMoney, float pSpeed, Vector3 pSpawn)
    {
        base.Initialize(pID, pMaxHealth, pMoney, pSpeed, pSpawn);
        gameObject.name = pID;

        anim = GetComponent<Animator>();
        myCollider = GetComponent<AbstractCollider>();
    }
    public void Initialize(EnemyProperties properties)
    {
        Initialize(properties.id, properties.maxHealth, properties.money, properties.moveSpeed, properties.spawnLocation);
    }

    public override void TakeAttack(int pDamage)
    {
        base.TakeAttack(pDamage);
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
    public override void RemoveFromGame()
    {
        base.RemoveFromGame();

        foreach (var observer in observers.ToArray())
            if (observers.Contains(observer))
                observer.OnCompleted();

        Destroy(gameObject);
    }
    public void Update()
    {
        if (moveAgent.IsMoving())
        {
            anim.SetFloat("Speed", moveAgent.GetCurrentSpeed() / moveAgent.GetMoveSpeed() );
        }
    }

    protected override void setupMoveAgent(Vector3 pSpawn, float pSpeed)
    {
        moveAgent = GetComponent<iMovementController>();
        moveAgent.SetPosition(pSpawn);
        moveAgent.SetMovementSpeed(pSpeed);

        moveAgent.SetDestination(new Vector3(75.0f, 2, -45.0f));
    }
}
