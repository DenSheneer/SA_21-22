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

    List<Enemy> enemies = new List<Enemy>();

    protected Timer _tickTimer;
    public void Initialize()
    {
        setupTimer();
    }
    public void SpawnEnemy()
    {
        Enemy newEnemy = createNewEnemy();
        if (newEnemy != null)
        {
            newEnemy.OnDeath += removeEnemyFromList;
            enemies.Add(newEnemy);
            currentSpawns++;
        }else { System.Diagnostics.Debug.WriteLine("newEnemy was null."); }
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
            _tickTimer.Initialize(_spawnTickTime, SpawnEnemy, true);
            _tickTimer.IsPaused = false;
        } else { System.Diagnostics.Debug.WriteLine("tickTimer was null."); }
    }
    /// <summary>
    /// Returns an enemy with variabes set. Implement in concrete class.
    /// </summary>
    protected abstract Enemy createNewEnemy();

    /// <summary>
    /// Returns definition for _tickTimer. Timer's variables could be set here or elsewhere.
    /// </summary>
    protected abstract Timer defineTickTimer();
}
