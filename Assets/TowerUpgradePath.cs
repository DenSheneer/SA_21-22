using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerUpgradePath : ScriptableObject
{
    [SerializeField]
    List<TowerProperties> powerProperties;
    [SerializeField]
    List<TowerBuildProperties> buildProperties;
    [SerializeField]
    uint addPoisonCosts;
    [SerializeField]
    uint addAOE_Costs;

    public TowerUpgradePath GetCopy()
    {
        return Instantiate(this);
    }

    public TowerProperties NextPowerTier(int currentTier)
    {
        int nextIndex = currentTier;
        if (powerProperties.Count > nextIndex)
        {
            return powerProperties[nextIndex];
        }
        return null;
    }
    public TowerProperties FirstPowerTier()
    {
        if (powerProperties.Count > 0)
        {
            return powerProperties[0];
        }
        return null;
    }

    public TowerBuildProperties NextBuildingTier(int currentTier)
    {
        int nextIndex = currentTier;
        if (buildProperties.Count > nextIndex)
        {
            return buildProperties[nextIndex];
        }
        return null;
    }
    public TowerBuildProperties FirstBuildTier()
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
        return powerProperties.Count;
    }
}
