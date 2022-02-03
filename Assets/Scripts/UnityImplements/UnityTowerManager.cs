using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTowerManager : TowerManager
{

    protected override Tower defineTower(TowerUpgradePath upgradePath, Vector3 position)
    {
        UnityTower unityTower = Instantiate(Resources.Load<UnityTower>("Tower"));
        if (unityTower != null)
        {
            unityTower.Initialize(upgradePath);

            unityTower.transform.position = position;
        }

        return unityTower;
    }

    private void Awake()
    {
        Initialize();
    }
}
