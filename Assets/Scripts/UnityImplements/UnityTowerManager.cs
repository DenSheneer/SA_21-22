using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTowerManager : TowerManager
{
    [SerializeField]
    TowerProperties towerProperties;

    [SerializeField]
    TowerBuildProperties weak;

    [SerializeField]
    TowerBuildProperties normal;

    [SerializeField]
    TowerBuildProperties strong;

    private void Start()
    {
        Initialize();
    }
    protected override iTowerSelector setupTowerSelector()
    {
        return GetComponent<UnityClickSelector>();
    }

    protected override Tower defineTower(TowerProperties towerProperties, Vector3 position)
    {
        UnityTower unityTower = Instantiate(Resources.Load<UnityTower>("Tower"));
        if (unityTower != null)
        {
            unityTower.Initialize(towerProperties);

            unityTower.transform.position = position;
        }

        return unityTower;
    }
}
