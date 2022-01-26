using System.Collections;
using System.Collections.Generic;

public abstract class TowerManager : UnityEngine.MonoBehaviour
{
    iTowerSelector towerSelector;
    protected List<Tower> towers;

    private System.Action<Tower> OnTowerSpawn;

    protected void Initialize()
    {
        setupTowerSelector();
        towers = new List<Tower>();
    }

    public void SpawnTower(TowerProperties towerProperties)
    {
        Tower newTower = defineTower(towerProperties);
        if (newTower != null)
        {
            towers.Add(newTower);
        }
    }

    public void SetExistingTowerProperties(Tower tower, TowerProperties towerProperties)
    {
        tower.SetProperties(towerProperties);
    }
    public void SubcribeToTowerSpawn(System.Action<Tower> onSpawn) { OnTowerSpawn += onSpawn; }
    public void UnsubscribeFromTowerSpawn(System.Action<Tower> onSpawn) { OnTowerSpawn -= onSpawn; }

    public Tower[] GetTowers() { return towers.ToArray(); }
    protected abstract void setupTowerSelector();
    protected abstract Tower defineTower(TowerProperties towerProperties);
}
