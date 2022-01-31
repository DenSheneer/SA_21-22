using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerUpgradeCosts : ScriptableObject
{
    [SerializeField]
    uint[] costs;

    public uint[] GetCosts { get { return costs; } }
}
