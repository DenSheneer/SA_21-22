using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Timer))]
public class UnityEnemySpawnHandler : EnemySpawnHandler
{
    [SerializeField]
    List<EnemyProperties> enemyTypes = new List<EnemyProperties>();
    [SerializeField]
    int enemyType = 0;

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
        unityEnemy.Initialize(enemyTypes[enemyType]);
        return unityEnemy;
    }
    protected override Timer defineTickTimer()
    {
        Timer newTimer = GetComponent<Timer>();
        return newTimer;
    }
}
