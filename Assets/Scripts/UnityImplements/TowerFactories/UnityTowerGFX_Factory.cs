using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTowerGFX_Factory : MonoBehaviour, iTowerGFX_Factory
{
    [SerializeField]
    protected MeshFilter towerBase, towerMiddle, towerTop;

    string path = "";

    public void AssembleGFX(TOWER_TIER towerTier) 
    {
        switch(towerTier)
        {
            case TOWER_TIER.weak:
                path = "Towers/BuildProperties/weak";
                break;
            case TOWER_TIER.normal:
                path = "Towers/BuildProperties/normal";
                break;
            case TOWER_TIER.strong:
                path = "Towers/BuildProperties/strong";
                break;
        }
        Draw();
    }
    public void Draw()
    {
        TowerBuildProperties towerBuildProperties = Resources.Load<TowerBuildProperties>(path);
        if (towerBuildProperties != null)
        {
            towerBase.mesh = towerBuildProperties.TowerBase;
            towerMiddle.mesh = towerBuildProperties.TowerMiddle;
            towerTop.mesh = towerBuildProperties.TowerTop;
        }
    }
}
