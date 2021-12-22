using UnityEngine;
using UnityEngine.AI;

public class UnityEnemy : Enemy
{
    //[SerializeField]
    //protected string ID;
    //int maxHealth, money;
    //[SerializeField]
    //float speed;


    //public void Start()
    //{
    //    //Initialize(ID, maxHealth, money, speed, Vector3.zero);
    //}
    Vector3 Spawn;

    public void Initialize(string pID, int pMaxHealth, int pMoney, float pSpeed, Vector3 pSpawn)
    {
        _ID = pID;
        _maxHealth = pMaxHealth;
        _currentHealth = pMaxHealth;
        _money = pMoney;
        moveAgent = GetComponent<MoveAgent>();
        moveAgent.SetPosition(pSpawn);
        moveAgent.SetMovementSpeed(pSpeed);        
        moveAgent.SetDestination(new Vector3(65.0f, 2, -65.0f));


        Spawn = pSpawn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = Vector3.zero;
        }
    }
}
