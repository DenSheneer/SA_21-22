using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnPool : ScriptableObject
{
    [SerializeField]
    public int spawns;

    [SerializeField]
    public List<enemyTypePoolSizeShare> poolSizes = new List<enemyTypePoolSizeShare>();

    List<EnemyProperties> spawnPool = null;

    public int SpawnCount { get { return spawns; } }
    public EnemyProperties RandomPullFromPool()
    {
        produceSpawnPool();
        int randomNr = Random.Range(0, spawnPool.Count);
        return spawnPool[randomNr];
    }
    private void produceSpawnPool()
    {
        spawnPool = new List<EnemyProperties>();
        foreach (enemyTypePoolSizeShare pair in poolSizes)
        {
            for (int i = 0; i < pair.PoolShareSize; i++)
            {
                spawnPool.Add(pair.TypeProperties);
            }
        }
    }
}
[System.Serializable]
public struct enemyTypePoolSizeShare
{
    public EnemyProperties TypeProperties;
    public int PoolShareSize;
}
