using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : UnityEngine.MonoBehaviour
{
    public TowerUpgradePath upgradePath;
    protected TowerProperties powerProperties;
    protected TowerBuildProperties buildProperties;

    protected Timer tickTimer;
    protected iTowerGFX_Factory GraphicsBuilder;

    private Dictionary<int, Enemy> inRangeEnemies;
    private int currentTier = 0;

    public virtual void Initialize(TowerUpgradePath upgradePath)
    {
        GraphicsBuilder = defineGraphicsBuilder();
        tickTimer = defineTickTimer();

        this.upgradePath = upgradePath.GetCopy();
        TowerProperties firstPowerTier = upgradePath.FirstPowerTier();
        if (firstPowerTier != null) { powerProperties = firstPowerTier; }
        TowerBuildProperties firstBuildTier = upgradePath.FirstBuildTier();
        if (firstBuildTier != null) { buildProperties = firstBuildTier; }

        if (tickTimer != null)
        {
            tickTimer.Initialize(powerProperties.attackTick, attack, true);
            tickTimer.IsPaused = false;
        }
        if (GraphicsBuilder != null)
        {
            GraphicsBuilder.AssembleGFX(buildProperties);
        }
    }

    public void Upgrade()
    {
        TowerProperties nextPowerTier = upgradePath.NextPowerTier(currentTier);
        if (nextPowerTier != null)
        {
            powerProperties = nextPowerTier;
        }
        TowerBuildProperties nextBuildTier = upgradePath.NextBuildingTier(currentTier);
        if (nextBuildTier != null)
        {
            buildProperties = nextBuildTier;
            GraphicsBuilder.AssembleGFX(buildProperties);
        }
        currentTier++;
    }

    private void attack()
    {
        iAttackable target = null;
        Enemy[] inRangeEnemies = getInRangeEnemies(EnemySpawnHandler.Instance.enemies.ToArray());
        switch (powerProperties.attackMode)
        {
            case ATTACK_MODE.RANDOM:
                target = selectRandomInrangeEnemy(inRangeEnemies);
                break;

            case ATTACK_MODE.NEAREST_TARGET:
                target = selectNearestEnemy(inRangeEnemies);
                break;
        }
        if (target != null) { attackTarget(target); }
    }
    void attackTarget(iAttackable target)
    {
        target.TakeAttack(powerProperties.damage);
    }
    Enemy selectNearestEnemy(Enemy[] enemies)
    {
        if (enemies.Length > 0)
        {
            Enemy nearest = null;
            float nearestDist = -1.0f;

            foreach (Enemy enemy in enemies)
            {
                float thisDist = Vector3.Distance(enemy.transform.position, transform.position);
                if (nearestDist < 0.0f || thisDist < nearestDist)
                {
                    nearestDist = thisDist;
                    nearest = enemy;
                }
            }
            return nearest;
        }
        return null;
    }
    Enemy selectRandomInrangeEnemy(Enemy[] enemies)
    {
        if (enemies.Length > 0)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            return enemies[randomIndex];
        }
        return null;
    }

    Enemy[] getInRangeEnemies(Enemy[] enemies)
    {
        List<Enemy> inRangeEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            float thisDist = Vector3.Distance(enemy.transform.position, transform.position);
            if (thisDist <= powerProperties.attackRadius)
            {
                inRangeEnemies.Add(enemy);
            }
        }
        return inRangeEnemies.ToArray();
    }

    protected abstract Timer defineTickTimer();

    protected abstract iTowerGFX_Factory defineGraphicsBuilder();

    public bool IsMaxTier()
    {
        if (currentTier >= upgradePath.MaxTier())
        {
            return true;
        }
        return false;
    }
    public int Tier { get { return currentTier; } }
    public string TierName { get { return powerProperties.tierName; } }

    public TowerUpgradePath UpgradePath { get { return upgradePath; } }
    public TowerProperties Properties { get { return powerProperties; } }

}

public enum ATTACK_MODE
{
    RANDOM = 0,
    NEAREST_TARGET = 1
}
