using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class EnemySpawnHandler : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    public List<Enemy> enemies = new List<Enemy>();

    private List<System.Numerics.Vector3> availableSpawns;
    int[] spawnsPerWave = { 10, 15, 25 };
    int currentSpawnsLeft;
    float spawnTickTime;

    iTimer tickTimer;
    public void Start()
    {
        setupTimer();
    }
    private void SpawnEnemy()
    {
        enemies.Add(new PrototypeEnemy());
    }
    protected void setupTimer()
    {
        tickTimer = GetComponent<iTimer>();
        tickTimer.Init(3.0f, SpawnEnemy, true);
        tickTimer.Start();
    }

}
