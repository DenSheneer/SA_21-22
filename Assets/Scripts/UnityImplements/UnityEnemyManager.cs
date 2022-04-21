using UnityEngine;
using System.Collections.Generic;
public class UnityEnemyManager: EnemyManager
{
    [SerializeField] Vector3 enemyDestination;

    [SerializeField]
    public float spawnTickTime = 3.0f;

    private void Awake()
    {
        Initialize();
    }

    protected override Enemy produceEnemyObject(EnemyProperties properties)
    {
        UnityEnemy unityEnemy = Instantiate(Resources.Load<UnityEnemy>("Enemies/" + properties.typename));
        unityEnemy.Initialize(Instantiate(properties), enemyDestination);
        return unityEnemy;
    }
    protected override Unitytimer defineTickTimer()
    {
        Unitytimer newTimer = GetComponent<Unitytimer>();
        return newTimer;
    }
}


