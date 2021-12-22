using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnHandler : MonoBehaviour
{

    [ReadOnly]
    [SerializeField]
    int currentSpawns = 0;

    public List<Enemy> enemies = new List<Enemy>();

    private List<System.Numerics.Vector3> availableSpawns;
    int[] spawnsPerWave = { 10, 15, 25 };
    int currentSpawnsLeft;
    float spawnTickTime;

    Timer tickTimer;
    public void Start()
    {
        setupTimer();
    }
    private void SpawnEnemy()
    {
        UnityEnemy EnemyGO = Instantiate(Resources.Load<UnityEnemy>("Zombie"));
        EnemyGO.Initialize("zombie", 10, 10, 10.0f, new Vector3(65.0f, 3.0f, -45.0f));
        enemies.Add(EnemyGO);
        currentSpawns++;
    }
    protected void setupTimer()
    {
        tickTimer = GetComponent<Timer>();
        tickTimer.Init(1.0f, SpawnEnemy, true);
        tickTimer.Start();
    }

}
