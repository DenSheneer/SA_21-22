using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerManager : UnityEngine.MonoBehaviour
{
    protected Dictionary<Transform, Tower> towers;

    [SerializeField]
    protected TowerUpgradePath towerUpgradePath;

    protected void Initialize()
    {
        towers = new Dictionary<Transform, Tower>();
    }

    public void BuildOrUpgrade(Transform clickedSpace)
    {
        if (!towers.ContainsKey(clickedSpace))
        {
            SpawnTower(towerUpgradePath, clickedSpace.position);
        }
        else
        {
            UpgradeTower(clickedSpace);
        }
    }

    public Tower GetTowerByTransform(Transform key)
    {
        towers.TryGetValue(key, out Tower outTower);
        return outTower;
    }

    public void SpawnTower(TowerUpgradePath upgradePath, Vector3 position)
    {
        Tower newTower = defineTower(upgradePath, position);
        if (newTower != null)
        {
            towers.Add(newTower.transform, newTower);
        }
    }

    protected void UpgradeTower(Transform key)
    {
        towers.TryGetValue(key, out Tower tower);
        if (tower != null)
        {
            tower.Upgrade();
        }
    }
    public uint GetFirstBuildCosts()
    {
        return towerUpgradePath.NextPowerTier(-1).costs;
    }
    protected abstract Tower defineTower(TowerUpgradePath upgradePath, Vector3 position);
}
