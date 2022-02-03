using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour, IObserver<Transform>
{
    uint moneyBeforeReset = 0;
    uint killsbeforeReset = 0;
    uint lifesBeforeReset = 10;

    iTowerSelector selector;
    Transform selectedSpace;
    Tower selectedTower;

    [SerializeField]
    GameUI gameUI;

    [SerializeField]
    UI_Building build_UI;

    System.IDisposable unsubscriber;

    private void Awake()
    {
        selector = GetComponent<iTowerSelector>();
        gameUI.SetResetWaveButton(CancelWave);
        MoneyManager.Instance.OnMoneyChange += gameUI.UpdateMoney;
        UnityGameOverChecker.Instance.OnGameOver += CancelWave;
    }

    private void Start()
    {
        unsubscriber = selector.Subscribe(this);
        EnemySpawnHandler.Instance.OnWaveComplete += StartBuildPhase;
        EnemySpawnHandler.Instance.OnComplete += gameWon;
        build_UI.OnUpgradeButtonClick += tryUpgrade;
        build_UI.OnStartButtonClick += StartWave;
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
        gameUI.UpdateMoney(MoneyManager.Instance.Money);
        gameUI.OpenUI(UI_TYPE.building);
    }
    void gameWon()
    {
        Debug.Log("huzzah! you won!");
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error){}

    public void OnNext(Transform selectedSpace)
    {
        this.selectedSpace = selectedSpace;
        selectedTower = TowerManager.Instance.GetTowerByTransform(selectedSpace);
        build_UI.SelectTower(selectedSpace, selectedTower, TowerManager.Instance.GetFirstBuildCosts());
    }
    private void tryUpgrade()
    {
        bool transactionpassed = false;
        if (selectedTower != null)
        {
            transactionpassed = MoneyManager.Instance.RemoveMoney(selectedTower.UpgradePath.NextPowerTier(selectedTower.Tier).costs);
        }
        else
        {
            transactionpassed = MoneyManager.Instance.RemoveMoney(TowerManager.Instance.GetFirstBuildCosts());
        }
        if (transactionpassed)
        {
            TowerManager.Instance.BuildOrUpgrade(selectedSpace);
            build_UI.CloseUpgradeMenu();
        }
    }
}
