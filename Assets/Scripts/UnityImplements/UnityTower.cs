using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTower : Tower
{
    List<Enemy> inrangeEnemy;
    protected override Timer defineTickTimer()
    {
        return GetComponent<Timer>();
    }

    protected override iTowerGFX_Factory defineGraphicsBuilder()
    {
        return GetComponent<iTowerGFX_Factory>();
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
        if (enemy != null)
        {

        }
    }

    protected override Enemy[] getInRangeEnemies()
    {
        List<Enemy> inRange = new List<Enemy>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, powerProperties.attackRadius);
        foreach(Collider col in hitColliders)
        {
            col.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
            if (enemy != null) { inRange.Add(enemy); }
        }
        return inRange.ToArray();
    }
}
