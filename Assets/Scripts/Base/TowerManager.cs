using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerManager : UnityEngine.MonoBehaviour, IObservable<Tower>
{
    protected static TowerManager _instance;

    [SerializeField]
    protected Dictionary<Transform, Tower> towers;

    private List<IObserver<Tower>> observers;
    private iTowerSelector towerSelector;

    public static TowerManager Instance { get { return _instance; } }

    protected void Initialize()
    {
        observers = new List<IObserver<Tower>>();
        towerSelector = setupTowerSelector();
        towers = new Dictionary<Transform, Tower>();
    }

    public void BuildOrUpgrade(Transform clickedSpace)
    {
        if (!towers.ContainsKey(clickedSpace))
        {
            SpawnTower(Resources.Load<TowerProperties>("Towers/WeakTower"), clickedSpace.position);
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

    public void SpawnTower(TowerProperties towerProperties, Vector3 position)
    {
        Tower newTower = defineTower(towerProperties, position);
        if (newTower != null)
        {
            towers.Add(newTower.transform, newTower);
        }
        NotifyObservers(newTower);
    }

    protected void UpgradeTower(Transform key)
    {
        Tower tower = towers[key];
        TowerProperties tp = tower.Properties;
        if (tp.Tier != TOWER_TIER.strong)
        {
            TowerProperties newProperties = null;
            string path = "";
            switch (tp.Tier)
            {
                case TOWER_TIER.weak:
                    path = "Towers/NormalTower";
                    break;
                case TOWER_TIER.normal:
                    path = "Towers/StrongTower";
                    break;
            }
            newProperties = Resources.Load<TowerProperties>(path);

            if (newProperties != null)
                tower.SetProperties(newProperties);

            NotifyObservers(tower);
        }
    }

    protected abstract iTowerSelector setupTowerSelector();
    protected abstract Tower defineTower(TowerProperties towerProperties, Vector3 position);

    public void OnError(Exception error) { }

    private void NotifyObservers(Tower tower)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(tower);
        }
    }

    public IDisposable SusbscribeToSelector(System.IObserver<Transform> observer)
    {
        if (towerSelector != null)
        {
            return towerSelector.Subscribe(observer);
        }
        else
        {
            return null;
        }
    }

    public IDisposable Subscribe(System.IObserver<Tower> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<Tower>(observers, observer);
    }

}

public enum TOWER_TIER
{
    none = -1,
    weak = 0,
    normal = 1,
    strong = 2
}
