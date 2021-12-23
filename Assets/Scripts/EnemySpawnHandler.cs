using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn handler abstract base class. Inherits from Monobehaviour for Unity compatibility.
/// </summary>
public abstract class EnemySpawnHandler : MonoBehaviour
{
    protected List<System.Numerics.Vector3> availableSpawns;
    protected int[] _spawnsPerWave = { 10, 15, 25 };
    protected int currentSpawns = 0;
    protected int _currentSpawnsLeft;
    protected float _spawnTickTime = 3.0f;

    public List<Enemy> enemies = new List<Enemy>();

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
            enemies.Add(newEnemy);
            currentSpawns++;
        }else { System.Diagnostics.Debug.WriteLine("newEnemy was null."); }
    }
    protected void setupTimer()
    {
        _tickTimer = defineTickTimer();
        if (_tickTimer != null)
        {
            _tickTimer.Initialize(_spawnTickTime, SpawnEnemy, true);
            _tickTimer.Begin();
        } else { System.Diagnostics.Debug.WriteLine("tickTimer was null."); }
    }
    /// <summary>
    /// Returns an enemy with variabes set. Implement in concrete class.
    /// </summary>
    protected abstract Enemy createNewEnemy();

    /// <summary>
    /// Returns definition for _tickTimer. Variables could be set here or elsewhere.
    /// </summary>
    protected abstract Timer defineTickTimer();
}
