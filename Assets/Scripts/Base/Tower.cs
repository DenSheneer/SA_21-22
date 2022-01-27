using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : UnityEngine.MonoBehaviour
{
    protected int damage = 1;
    protected float attackTickTime = 1.0f;
    protected float attackRadius = 12.0f;
    protected Timer tickTimer;
    protected ATTACK_MODE attackMode;
    protected iTowerGraphicsBuilder GraphicsBuilder;

    private Dictionary<int, Enemy> inRangeEnemies;

    public virtual void Initialize(TowerProperties towerProperties)
    {
        GraphicsBuilder = defineGraphicsBuilder();
        tickTimer = defineTickTimer();
        if (tickTimer != null)
        {
            tickTimer.Initialize(attackTickTime, attack, true);
            tickTimer.IsPaused = false;
        }
        SetProperties(towerProperties);
    }
    public void SetProperties(TowerProperties towerProperties)
    {
        attackMode = towerProperties.AttackMode;
        damage = towerProperties.Damage;
        attackTickTime = towerProperties.AttackTick;
        attackRadius = towerProperties.AttackRadius;
        GraphicsBuilder.AssembleGFX(towerProperties.BuildProperties);
    }
    private void attack()
    {
        iAttackable target = null;
        Enemy[] inRangeEnemies = getInRangeEnemies(EnemySpawnHandler.Instance.enemies.ToArray());
        switch (attackMode)
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
        target.TakeAttack(damage);
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
            Debug.Log(nearestDist);
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
            if (thisDist <= attackRadius)
            {
                inRangeEnemies.Add(enemy);
            }
        }
        return inRangeEnemies.ToArray();
    }

    protected abstract Timer defineTickTimer();

    protected abstract iTowerGraphicsBuilder defineGraphicsBuilder();

}

public enum ATTACK_MODE
{
    RANDOM = 0,
    NEAREST_TARGET = 1
}
