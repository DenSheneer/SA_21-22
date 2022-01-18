using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Tower : UnityEngine.MonoBehaviour
{
    protected int damage = 1;
    protected float attackTickTime = 1.0f;
    protected float attackRadius = 10.0f;
    protected Timer tickTimer;
    private ATTACK_MODE attackMode = ATTACK_MODE.RANDOM;

    private Dictionary<int, Enemy> inRangeEnemies;

    protected virtual void Initialize(ATTACK_MODE pAttackMode, int pDamage, float pAttackTickTime, float pAttackRadius)
    {
        attackMode = pAttackMode;
        damage = pDamage;
        attackTickTime = pAttackTickTime;
        attackRadius = pAttackRadius;

        inRangeEnemies = new Dictionary<int, Enemy>();

        initializeCollider();
        tickTimer = defineTickTimer();
        if (tickTimer != null)
        {
            tickTimer.Initialize(attackTickTime, attack, true);
            tickTimer.IsPaused = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
        {
            bool firstTarget = false;
            if (inRangeEnemies.Count == 0) { firstTarget = true; }

            addEnemyToList(newEnemy);
            if (firstTarget) 
            {
                attack();
                tickTimer.ResetTimer();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy exitingEnemy))
        {
            removeEnemyFromList(exitingEnemy);
        }
    }
    private void attack()
    {
        if (inRangeEnemies.Count > 0)
        {
            iAttackable target = null;
            switch (attackMode)
            {
                case ATTACK_MODE.RANDOM:
                    target = selectRandomEnemy();
                    break;

                case ATTACK_MODE.NEAREST_TARGET:
                    target = selectNearestEnemy();
                    break;
            }
            if (target != null) { attackTarget(target); }
        }
    }
    void attackTarget(iAttackable target)
    {
        target.TakeAttack(damage);
    }
    void addEnemyToList(Enemy newEnemy)
    {
        if (!inRangeEnemies.ContainsKey(newEnemy.GetHashCode()))
        {
            inRangeEnemies.Add(newEnemy.GetHashCode(), newEnemy);
        }
        newEnemy.OnDeath += removeEnemyFromList;
    }
    void removeEnemyFromList(Enemy enemy)
    {
        if (inRangeEnemies.ContainsKey(enemy.GetHashCode()))
        {
            inRangeEnemies.Remove(enemy.GetHashCode());
        }
    }
    Enemy selectNearestEnemy()
    {
        Enemy nearest = null;
        float nearestDist = -1.0f;
        foreach (KeyValuePair<int, Enemy> pair in inRangeEnemies)
        {
            float thisDist = Vector3.Distance(pair.Value.transform.position, transform.position);
            if (nearestDist < 0.0f || thisDist < nearestDist)
            {
                nearestDist = thisDist;
                nearest = pair.Value;
            }
        }
        return nearest;
    }
    Enemy selectRandomEnemy()
    {
        int[] keys = inRangeEnemies.Keys.ToArray();
        int randomKey = keys[Random.Range(0, keys.Length)];
        return inRangeEnemies[randomKey];
    }

    protected abstract void initializeCollider();
    protected abstract Timer defineTickTimer();
}

public enum ATTACK_MODE
{
    RANDOM = 0,
    NEAREST_TARGET = 1
}
