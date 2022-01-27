using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTowerGraphicsBuilder : MonoBehaviour, iTowerGraphicsBuilder
{
    [SerializeField]
    MeshFilter towerBase, towerMiddle, towerTop;

    public void AssembleGFX(TowerBuildProperties towerBuildProperties)
    {
        towerBase.mesh = towerBuildProperties.TowerBase;
        towerMiddle.mesh = towerBuildProperties.TowerMiddle;
        towerTop.mesh = towerBuildProperties.TowerTop;
    }
}
