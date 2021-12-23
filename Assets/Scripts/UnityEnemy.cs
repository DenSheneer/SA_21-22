using UnityEngine;
using UnityEngine.AI;

public class UnityEnemy : Enemy
{
    Animator anim;
    public override void Initialize(string pID, int pMaxHealth, int pMoney, float pSpeed, Vector3 pSpawn)
    {
        base.Initialize(pID, pMaxHealth, pMoney, pSpeed, pSpawn);
        gameObject.name = pID;

        anim = GetComponent<Animator>();
    }

    public override void TakeAttack(int pDamage)
    {
        base.TakeAttack(pDamage);
    }
    public override void Die()
    {
        base.Die();
        Destroy(this);
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

        moveAgent.SetDestination(new Vector3(65.0f, 2, -65.0f));
    }
}
