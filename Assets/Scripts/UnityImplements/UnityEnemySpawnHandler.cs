using UnityEngine;

[RequireComponent(typeof(Timer))]
public class UnityEnemySpawnHandler : EnemySpawnHandler
{
    [SerializeField]
    public float spawnTickTime = 3.0f;
    private void Start()
    {
        _spawnTickTime = spawnTickTime;
        Initialize();
    }
    protected override Enemy createNewEnemy()
    {
        UnityEnemy unityEnemy = Instantiate(Resources.Load<UnityEnemy>("Zombie"));
        unityEnemy.Initialize("zombie", 350, 10, 10.0f, new Vector3(65.0f, 3.0f, -65.0f));
        return unityEnemy;
    }
    protected override Timer defineTickTimer()
    {
        Timer newTimer = GetComponent<Timer>();
        return newTimer;
    }
}
