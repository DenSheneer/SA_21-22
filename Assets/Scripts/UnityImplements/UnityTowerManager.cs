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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Initialize();
    }
}
