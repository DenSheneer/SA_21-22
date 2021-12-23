using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnityEnemySpawnHandler : EnemySpawnHandler
{
    [SerializeField]
    public float spawnTickTime = 3.0f;
    private void Start()
    {
        _spawnTickTime = spawnTickTime;
        Initialize();
    }
    protected override Enemy createNewEnemy()
    {
        UnityEnemy unityEnemy = Instantiate(Resources.Load<UnityEnemy>("Zombie"));
        unityEnemy.Initialize("zombie", 10, 10, 10.0f, new Vector3(65.0f, 3.0f, -45.0f));
        return unityEnemy;
    }
    protected override Timer defineTickTimer()
    {
        return GetComponent<Timer>();
    }
}
