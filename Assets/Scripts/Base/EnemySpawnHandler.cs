using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn handler abstract base class. Inherits from Monobehaviour for Unity compatibility.
/// </summary>
public abstract class EnemySpawnHandler : MonoBehaviour
{
    protected int currentSpawns = 0;
    protected float _spawnTickTime = 3.0f;
    protected Unitytimer _tickTimer;
    protected SpawnPool enemySpawnPool;
    protected int currentWave = 0;

    [SerializeField]
    protected List<SpawnPool> waves = new List<SpawnPool>();

    public List<Enemy> enemies = new List<Enemy>();
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

    public void onEnemyKill(Enemy enemy)
    {
        OnEnemyKill?.Invoke(enemy);
    }
    public void StartWave()
    {
        SpawnEnemyTick();
        _tickTimer.IsPaused = false;
    }
    public void SetWave(int waveNr)
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
        _tickTimer.ResetTimer();
        _tickTimer.IsPaused = true;
    }
    public void SpawnEnemyTick()
    {
        if (currentSpawns < enemySpawnPool.SpawnCount)
        {
            Enemy newEnemy = createNewRandomEnemy(enemySpawnPool);
            if (newEnemy != null)
            {
                newEnemy.OnDeath += onEnemyKill;
                enemies.Add(newEnemy);
                currentSpawns++;
                OnEnemySpawn?.Invoke(newEnemy);
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
        SetWave(currentWave);
        _tickTimer.ResetTimer();
        _tickTimer.IsPaused = true;
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

    protected void removeEnemyFromList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
    protected void setupTimer()
    {
        _tickTimer = defineTickTimer();
        if (_tickTimer != null)
        {
            _tickTimer.Initialize(_spawnTickTime, SpawnEnemyTick, true);
            _tickTimer.IsPaused = true;
        }
        else { System.Diagnostics.Debug.WriteLine("tickTimer was null."); }
    }
    /// <summary>
    /// Returns an enemy with variabes set. Implement in concrete class.
    /// </summary>
    protected abstract Enemy spawnEnemy(ENEMY_TYPE type);

    protected Enemy createNewRandomEnemy(SpawnPool spawnRules)
    {
        Enemy enemy = null;
        enemy = spawnEnemy(spawnRules.RandomPullFromPool());
        return enemy;
    }

    /// <summary>
    /// Returns definition for _tickTimer. Timer's variables could be set here or elsewhere.
    /// </summary>
    protected abstract Unitytimer defineTickTimer();
}
public enum ENEMY_TYPE
{
    Spider = 0,
    Zombie = 1,
    Boss = 2
}
