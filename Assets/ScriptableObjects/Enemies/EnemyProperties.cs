using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyProperties : ScriptableObject
{
    [SerializeField]
    public string typename;

    [SerializeField]
    public int maxHealth;
        
    [SerializeField]
    public uint money;

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public Vector3 spawnLocation;
}
