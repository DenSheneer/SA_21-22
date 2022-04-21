using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn handler abstract base class. Inherits from Monobehaviour for Unity compatibility.
/// </summary>
public abstract class EnemyManager : MonoBehaviour
{
    protected int currentSpawns = 0;
    protected Unitytimer tickTimer;
    protected SpawnPool enemySpawnPool;
    protected int currentWave = 0;

    [SerializeField]
    float minSpawnTickTime = 0.2f, maxSpawnTickTime = 1.5f;

    [SerializeField]
    protected List<SpawnPool> waves = new List<SpawnPool>();

    private List<Enemy> enemies = new List<Enemy>();
    public System.Action<Enemy> OnEnemyKill;
    public System.Action<Enemy> OnEnemySpawn;
    public System.Action OnWaveComplete;
    public System.Action OnComplete;
    public void Initialize()
    {
        setupTimer();
        enemySpawnPool = waves[currentWave];
    }
    public int SpawnsLeft { get { return enemySpawnPool.spawns - currentSpawns; } }

    private void onEnemyKill(Enemy enemy)
    {
        OnEnemyKill?.Invoke(enemy);
    }
    public void StartWave()
    {
        spawnEnemyTick();
        tickTimer.IsPaused = false;
    }
    private void setWave(int waveNr)
    {
        if (waves.Count > waveNr)
        {
            enemySpawnPool = waves[waveNr];
        }
    }
    public void ResetWave()
    {
        while (enemies.Count != 0)
        {
            Enemy toBeDeleted = enemies[enemies.Count - 1];
            removeEnemyFromList(toBeDeleted);
            toBeDeleted.Delete();
        }

        currentSpawns = 0;
        tickTimer.ResetTimer();
        tickTimer.IsPaused = true;
    }
    private void spawnEnemyTick()
    {
        if (currentSpawns < enemySpawnPool.SpawnCount)
        {
            if (enemySpawnPool != null)
            {
                Enemy newEnemy = createNewRandomEnemy(enemySpawnPool);
                if (newEnemy != null)
                {
                    newEnemy.OnDeath += onEnemyKill;
                    float randomSpawntime = Random.Range(minSpawnTickTime, maxSpawnTickTime);
                    tickTimer.SetTime(randomSpawntime);
                    enemies.Add(newEnemy);
                    currentSpawns++;
                    OnEnemySpawn?.Invoke(newEnemy);
                }
            }
        }
    }
    private void onWaveComplete()
    {
        if (currentWave == waves.Count - 1)
        {
            OnComplete?.Invoke();
        }
        else
        {
            nextWave();
        }
    }

    private void nextWave()
    {
        currentWave++;
        currentSpawns = 0;
        setWave(currentWave);
        tickTimer.ResetTimer();
        tickTimer.IsPaused = true;
        OnWaveComplete?.Invoke();
    }

    public void HandleKilledEnemy(Enemy enemy)
    {
        removeEnemyFromList(enemy);
        if (enemies.Count < 1)
        {
            onWaveComplete();
        }
    }

    public void HandleGoalReachedEnemy(Enemy enemy)
    {
        removeEnemyFromList(enemy);
        if (enemies.Count < 1)
        {
            onWaveComplete();
        }
    }

    private void removeEnemyFromList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
    private void setupTimer()
    {
        tickTimer = defineTickTimer();
        if (tickTimer != null)
        {
            tickTimer.Initialize(Random.Range(minSpawnTickTime, maxSpawnTickTime), spawnEnemyTick, true);
            tickTimer.IsPaused = true;
        }
        else { System.Diagnostics.Debug.WriteLine("tickTimer was null."); }
    }

    private Enemy createNewRandomEnemy(SpawnPool spawnRules)
    {
        Enemy enemy = null;
        enemy = produceEnemyObject(spawnRules.RandomPullFromPool());
        return enemy;
    }
    public int GetWaveNumber()
    {
        return currentWave + 1;
    }

    /// <summary>
    /// Returns an enemy with variabes set. Implement in concrete class.
    /// </summary>
    protected abstract Enemy produceEnemyObject(EnemyProperties properties);

    /// <summary>
    /// Creates definition for tickTimer. Timer's variables could be set here or elsewhere.
    /// </summary>
    protected abstract Unitytimer defineTickTimer();
}
public enum ENEMY_TYPE
{
    Spider = 0,
    Zombie = 1,
    Boss = 2
}
