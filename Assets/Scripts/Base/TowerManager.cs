using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerManager : UnityEngine.MonoBehaviour, IObserver<Transform>
{
    iTowerSelector towerSelector;

    [SerializeField]
    protected Dictionary<Transform, Tower> towers;
    private Action<Tower> OnTowerSpawn;

    private IDisposable unsubscriber;

    protected void Initialize()
    {
        towerSelector = setupTowerSelector();
        unsubscriber = towerSelector.Subscribe(this);
        towers = new Dictionary<Transform, Tower>();
    }

    public void OnNext(Transform clickedSpace)
    {
        if (!towers.ContainsKey(clickedSpace))
        {
            SpawnTower(Resources.Load<TowerProperties>("Towers/NormalTower"), clickedSpace.position);
        }
        else { UpgradeTower(clickedSpace); }
    }

    public void SpawnTower(TowerProperties towerProperties, Vector3 position)
    {
        Tower newTower = defineTower(towerProperties, position);
        if (newTower != null)
        {
            towers.Add(newTower.transform, newTower);
        }
    }

    protected void UpgradeTower(Transform key)
    {
        Debug.Log("upgraded tower " + towers[key].name);
        //SetExistingTowerProperties(towers[key], Resources.Load<TowerBuildProperties>("/BuildProperties/strong"));
    }

    public void SetExistingTowerProperties(Tower tower, TowerProperties towerProperties)
    {
        tower.SetProperties(towerProperties);
    }
    public void SubcribeToTowerSpawn(System.Action<Tower> onSpawn) { OnTowerSpawn += onSpawn; }
    public void UnsubscribeFromTowerSpawn(System.Action<Tower> onSpawn) { OnTowerSpawn -= onSpawn; }

    protected abstract iTowerSelector setupTowerSelector();
    protected abstract Tower defineTower(TowerProperties towerProperties, Vector3 position);

    public void OnCompleted()
    {
        unsubscriber.Dispose();
    }

    public void OnError(Exception error) { }
}

public enum TOWER_TIER
{
    weak = 0,
    normal = 1,
    strong = 2
}
