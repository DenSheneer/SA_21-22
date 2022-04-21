using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GameOverChecker : MonoBehaviour
{
    public System.Action<Enemy> OnEnemyGoalReach;

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();

        rb.isKinematic = true;
        col.isTrigger = true;
        rb.useGravity = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enteringEnemy))
        {
            enteringEnemy.GoalReached();
            OnEnemyGoalReach?.Invoke(enteringEnemy);
        }
    }
}
