using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerVisualProperties : ScriptableObject
{
    [SerializeField]
    public string tierName = "";

    [SerializeField]
    Mesh towerBase;

    [SerializeField]
    Mesh towerMiddle;

    [SerializeField]
    Mesh towerTop;

    public Mesh TowerBase { get { return towerBase; } }
    public Mesh TowerMiddle { get { return towerMiddle; } }
    public Mesh TowerTop { get { return towerTop; } }
}
