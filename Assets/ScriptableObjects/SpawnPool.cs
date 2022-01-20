using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnPool : ScriptableObject
{
    [SerializeField]
    public int spawns;

    [SerializeField]
    public int maxStrongEnemies;

    [SerializeField]
    public int strongEnemPool;

    [SerializeField]
    public int medEnemPool;

    [SerializeField]
    public int weakEnemPool;

    public int PoolSize() { return weakEnemPool + medEnemPool + strongEnemPool; }
    public ENEMY_TYPE RandomPullFromPool()
    {
        if (PoolSize() > 0)
        {
            ENEMY_TYPE[] pool = new ENEMY_TYPE[PoolSize()];

            int i = 0;
            while (i < weakEnemPool) { pool[i] = ENEMY_TYPE.Spider; i++; }
            while (i < weakEnemPool + medEnemPool) { pool[i] = ENEMY_TYPE.Zombie; i++; }
            while (i < weakEnemPool + medEnemPool + strongEnemPool)
            { pool[i] = ENEMY_TYPE.Boss; i++; }

            int randomNr = Random.Range(0, pool.Length);
            return pool[randomNr];
        }
        else
        {
            return ENEMY_TYPE.Zombie;
        }
    }
}
