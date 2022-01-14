using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyProperties : ScriptableObject
{
    [SerializeField]
    public string id;

    [SerializeField]
    public int maxHealth, money;

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public Vector3 spawnLocation;
}
