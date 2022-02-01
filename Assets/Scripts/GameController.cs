using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    uint moneyBeforeReset = 0;
    uint killsbeforeReset = 0;
    uint lifesBeforeReset = 10;


    [SerializeField]
    GameUI gameUI;

    private void Awake()
    {
        gameUI.SetResetWaveButton(CancelWave);
        gameUI.SetStartWaveButton(StartWave);
        MoneyManager.Instance.OnMoneyChange += gameUI.UpdateMoney;
        UnityGameOverChecker.Instance.OnGameOver += CancelWave;
    }

    private void Start()
    {
        gameUI.UpdateMoney(MoneyManager.Instance.Money);
        StartBuildPhase();
    }
    void StartWave()
    {
        gameUI.OpenUI(UI_TYPE.gameplay);
        moneyBeforeReset = MoneyManager.Instance.Money;
        killsbeforeReset = EnemySpawnHandler.Instance.Kills;
        lifesBeforeReset = UnityGameOverChecker.Instance.Lifes;
        EnemySpawnHandler.Instance.StartWave();
    }
    void CancelWave()
    {
        UnityGameOverChecker.Instance.SetLifes(lifesBeforeReset);
        MoneyManager.Instance.SetMoney(moneyBeforeReset);
        EnemySpawnHandler.Instance.SetKills(killsbeforeReset);
        EnemySpawnHandler.Instance.ResetWave();
        StartBuildPhase();
    }

    void StartBuildPhase()
    {
        gameUI.OpenUI(UI_TYPE.building);
    }
}
