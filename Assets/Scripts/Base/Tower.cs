using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : UnityEngine.MonoBehaviour
{
    public TowerUpgradePath upgradePath;
    protected TowerPowerProperties powerProperties;
    protected TowerVisualProperties buildProperties;

    protected Unitytimer tickTimer;
    protected iTowerGFX_Factory GraphicsBuilder;

    private int currentTier = 0;
    private bool poisonEnabled = false;
    private bool aoeAttackEnabled = false;

    public virtual void Initialize(TowerUpgradePath upgradePath)
    {
        GraphicsBuilder = defineGraphicsBuilder();
        tickTimer = defineTickTimer();

        this.upgradePath = upgradePath.GetCopy();
        TowerPowerProperties firstPowerTier = upgradePath.FirstPowerTier();
        if (firstPowerTier != null) { powerProperties = firstPowerTier; }
        TowerVisualProperties firstBuildTier = upgradePath.FirstBuildTier();
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
        TowerPowerProperties nextPowerTier = upgradePath.NextPowerTier(currentTier);
        if (nextPowerTier != null)
        {
            powerProperties = nextPowerTier;
        }
        TowerVisualProperties nextBuildTier = upgradePath.NextBuildingTier(currentTier);
        if (nextBuildTier != null)
        {
            buildProperties = nextBuildTier;
            GraphicsBuilder.AssembleGFX(buildProperties);
        }
        currentTier++;
    }
    public bool PoisonEnabled
    {
        get { return poisonEnabled; }
        set { poisonEnabled = value; }
    }
    public bool AOE_Enabled
    {
        get { return aoeAttackEnabled; }
        set { aoeAttackEnabled = value; }
    }

    private void attack()
    {
        iAttackable target = null;
        Enemy[] inRangeEnemies = getInRangeEnemies(transform.position, powerProperties.attackRadius);
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
        if (aoeAttackEnabled)
        {
            Enemy[] inAOE_Range = getInRangeEnemies(target.GetPosition(), powerProperties.aoeAttackRange);
            if (inAOE_Range.Length > 0)
            {
                for (int i = 0; i < inAOE_Range.Length; i++)
                {
                    inAOE_Range[i].TakeAttack(powerProperties.damage);
                    if (poisonEnabled)
                    {
                        if (inAOE_Range[i] != null)
                        {
                            inAOE_Range[i].TakeStatusAttack(powerProperties.poisonDamage);
                        }
                    }
                }
            }
        }
        else
        {
            target.TakeAttack(powerProperties.damage);
            if (poisonEnabled)
            {
                target.TakeStatusAttack(powerProperties.poisonDamage);
            }
        }
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

    protected abstract Enemy[] getInRangeEnemies(Vector3 position, float radius);

    protected abstract Unitytimer defineTickTimer();

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
    public TowerPowerProperties Properties { get { return powerProperties; } }

}

public enum ATTACK_MODE
{
    RANDOM = 0,
    NEAREST_TARGET = 1
}
