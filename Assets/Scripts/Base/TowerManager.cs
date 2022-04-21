using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerManager : UnityEngine.MonoBehaviour
{
    private Dictionary<int, Tower> activeTowers = new Dictionary<int, Tower>();

    [SerializeField]
    protected TowerUpgradePath towerUpgradePath;

    private List<IObserver<Tower>> observers;

    protected void Initialize()
    {
        observers = new List<IObserver<Tower>>();
    }
    public void UpgradeTower(Tower tower)
    {
        if (tower != null)
        {
            if (tower.UpgradePath != null)
            {
                tower.Upgrade();
                if (!activeTowers.ContainsKey(tower.GetHashCode()))
                {
                    activeTowers.Add(tower.GetHashCode(), tower);
                }
            }
        }
    }
    public void EnableTowerAttacks()
    {
        foreach (KeyValuePair<int, Tower> pair in activeTowers)
        {
            Tower tower = pair.Value;
            if (tower != null)
            {
                tower.StartFiring();
            }
        }
    }
    public void DisableTowerAttacks()
    {
        foreach (KeyValuePair<int, Tower> pair in activeTowers)
        {
            Tower tower = pair.Value;
            if (tower != null)
            {
                tower.StopFiring();
            }
        }
    }
}
