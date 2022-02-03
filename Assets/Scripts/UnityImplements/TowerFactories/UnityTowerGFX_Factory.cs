using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTowerGFX_Factory : MonoBehaviour, iTowerGFX_Factory
{
    [SerializeField]
    protected MeshFilter towerBase, towerMiddle, towerTop;

    public void AssembleGFX(TowerBuildProperties tbp) 
    {
        if (tbp != null)
        {
            towerBase.mesh = tbp.TowerBase;
            towerMiddle.mesh = tbp.TowerMiddle;
            towerTop.mesh = tbp.TowerTop;
        }
    }
}
