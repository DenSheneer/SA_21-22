using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Timer))]
public class UnityEnemySpawnHandler : EnemySpawnHandler
{
    [SerializeField]
    List<EnemyProperties> enemyTypes = new List<EnemyProperties>();
    [SerializeField]
    SpawnPool spawnRule;

    [SerializeField]
    public float spawnTickTime = 3.0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _spawnTickTime = spawnTickTime;
        Initialize(spawnRule);
    }

    protected override Enemy spawnEnemy(ENEMY_TYPE type)
    {
        UnityEnemy unityEnemy = Instantiate(Resources.Load<UnityEnemy>(enemyTypes[(int)type].id));
        unityEnemy.Initialize(enemyTypes[(int)type]);
        return unityEnemy;
    }
    protected override Timer defineTickTimer()
    {
        Timer newTimer = GetComponent<Timer>();
        return newTimer;
    }
}


