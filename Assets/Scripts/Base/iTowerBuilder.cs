using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iTowerBuilder
{
    void BuildTower(TowerProperties towerProperties);
    void ChangeExistingTower(Tower tower, TowerProperties towerProperties);
}
