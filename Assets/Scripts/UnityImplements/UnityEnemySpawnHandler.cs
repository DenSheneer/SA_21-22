using UnityEngine;
using System.Collections.Generic;
public class UnityEnemySpawnHandler : EnemySpawnHandler
{
    [SerializeField]
    List<EnemyProperties> enemyTypes = new List<EnemyProperties>();

    [SerializeField]
    public float spawnTickTime = 3.0f;

    private void Awake()
    {
        _spawnTickTime = spawnTickTime;
        Initialize();
    }

    protected override Enemy spawnEnemy(ENEMY_TYPE type)
    {
        UnityEnemy unityEnemy = Instantiate(Resources.Load<UnityEnemy>(enemyTypes[(int)type].id));
        unityEnemy.Initialize(enemyTypes[(int)type]);
        return unityEnemy;
    }
    protected override Unitytimer defineTickTimer()
    {
        Unitytimer newTimer = GetComponent<Unitytimer>();
        return newTimer;
    }
}


