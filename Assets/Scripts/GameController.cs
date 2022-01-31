using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int enemiesPassedBeforeReset = 10;

    uint moneyBeforeReset = 0;

    [SerializeField]
    UnityEnemyGoal enemyGoal;

    [SerializeField]
    GameUI gameUI;

    private void Awake()
    {
        gameUI.SetResetWaveButton(CancelWave);
        gameUI.SetStartWaveButton(StartWave);
    }

    private void Start()
    {
        StartBuildPhase();
    }
    void StartWave()
    {
        gameUI.OpenUI(UI_TYPE.gameplay);
        moneyBeforeReset = MoneyManager.Instance.Money;
        enemiesPassedBeforeReset = enemyGoal.EnemiesPassed;
        EnemySpawnHandler.Instance.StartWave();
    }
    void CancelWave()
    {
        enemyGoal.EnemiesPassed = enemiesPassedBeforeReset;
        MoneyManager.Instance.SetMoney(moneyBeforeReset);
        EnemySpawnHandler.Instance.ResetWave();
        StartBuildPhase();
    }

    void StartBuildPhase()
    {
        gameUI.OpenUI(UI_TYPE.building);
    }

}
