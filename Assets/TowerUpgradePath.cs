using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerUpgradePath : ScriptableObject
{
    [SerializeField]
    List<TowerPowerProperties> powerProperties;
    [SerializeField]
    List<TowerVisualProperties> buildProperties;
    [SerializeField]
    uint addPoisonCosts;
    [SerializeField]
    uint addAOE_Costs;

    public TowerUpgradePath GetCopy()
    {
        return Instantiate(this);
    }

    public TowerPowerProperties NextPowerTier(int currentTier)
    {
        int nextIndex = currentTier + 1;
        if (powerProperties.Count > nextIndex)
        {
            return powerProperties[nextIndex];
        }
        return null;
    }
    public TowerPowerProperties FirstPowerTier()
    {
        if (powerProperties.Count > 0)
        {
            return powerProperties[0];
        }
        return null;
    }

    public TowerVisualProperties NextBuildingTier(int currentTier)
    {
        int nextIndex = currentTier + 1;
        if (buildProperties.Count > nextIndex)
        {
            return buildProperties[nextIndex];
        }
        return null;
    }
    public TowerVisualProperties FirstBuildTier()
    {
        if (buildProperties.Count > 0)
        {
            return buildProperties[0];
        }
        return null;
    }

    public uint GetAddPoisonCosts()
    {
        return addPoisonCosts;
    }
    public uint GetAddAOE_Costs()
    {
        return addAOE_Costs;
    }

    public int MaxTier()
    {
        return powerProperties.Count - 1;
    }
}
