using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour, IObserver<Transform>
{
    uint moneyBeforeReset = 0;

    uint currentKills = 0;
    uint killsbeforeReset = 0;

    uint currentLifes = 10;
    uint lifesBeforeReset = 0;

    iTowerSelector selector;
    Transform selectedSpace;
    Tower selectedTower;

    [SerializeField] TowerManager towerManager;
    [SerializeField] EnemySpawnHandler enemyManager;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] UnityGameOverChecker gameOverChecker;
    [SerializeField] UI_Permanent gameUI;
    [SerializeField] UI_Building build_UI;
    [SerializeField] UI_Gameplay gameplay_UI;

    System.IDisposable unsubscriber;

    private void Awake()
    {
        selector = GetComponent<iTowerSelector>();
        moneyManager.OnMoneyChange += gameUI.UpdateMoney;
        gameOverChecker.OnEnemyGoalReach += handleGoalReachedEnemy;
        gameplay_UI.OnResetButtonClick += CancelWave;
        build_UI.OnUpgradeButtonClick += tryUpgrade;
        build_UI.OnStartButtonClick += StartWave;
    }

    private void Start()
    {
        enemyManager.OnWaveComplete += StartBuildPhase;
        enemyManager.OnComplete += gameWon;
        enemyManager.OnEnemyKill += handleEnemyKilled;
        enemyManager.OnEnemySpawn += handleEnemySpawned;
        unsubscriber = selector.Subscribe(this);

        StartBuildPhase();
    }
    void StartWave()
    {
        gameUI.OpenUI(UI_TYPE.gameplay);
        moneyBeforeReset = moneyManager.Money;
        killsbeforeReset = currentKills;
        lifesBeforeReset = currentLifes;
        enemyManager.StartWave();
    }
    void CancelWave()
    {
        moneyManager.SetMoney(moneyBeforeReset);
        currentLifes = lifesBeforeReset;
        currentKills = killsbeforeReset;
        enemyManager.ResetWave();
        StartBuildPhase();
    }

    void StartBuildPhase()
    {
        gameUI.OpenUI(UI_TYPE.building);
        gameplay_UI.UpdateKills(currentKills);
        gameUI.UpdateLifes(currentLifes);
        gameUI.UpdateMoney(moneyManager.Money);
    }

    /// <summary>
    /// gameWon should be called from the enemySpawnHandler when there are no enemies or waves of enemies left.
    /// </summary>
    void gameWon()
    {
        Debug.Log("huzzah! you won!");
    }

    public void OnNext(Transform selectedSpace)
    {
        this.selectedSpace = selectedSpace;
        selectedTower = towerManager.GetTowerByTransform(selectedSpace);
        build_UI.SelectTower(selectedSpace, selectedTower, towerManager.GetFirstBuildCosts());
    }
    private void tryUpgrade()
    {
        bool transactionpassed = false;
        if (selectedTower != null)
        {
            transactionpassed = moneyManager.RemoveMoney(selectedTower.UpgradePath.NextPowerTier(selectedTower.Tier).costs);
        }
        else
        {
            transactionpassed = moneyManager.RemoveMoney(towerManager.GetFirstBuildCosts());
        }
        if (transactionpassed)
        {
            towerManager.BuildOrUpgrade(selectedSpace);
            build_UI.CloseUpgradeMenu();
        }
    }
    /// <summary>
    /// handleEnemyKilled should be called from an enemy that was killed by the player.
    /// </summary>
    private void handleEnemyKilled(Enemy enemy)
    {
        currentKills++;
        moneyManager.AddMoney(enemy.GetMoney);
        gameUI.UpdateMoney(moneyManager.Money);
        gameplay_UI.UpdateKills(currentKills);
        enemyManager.HandleKilledEnemy(enemy);
    }
    /// <summary>
    /// handleEnemySpawned should be called from the EnemySpawnHandler.
    /// </summary>
    private void handleEnemySpawned(Enemy enemy)
    {
        gameplay_UI.UpdateSpawns(enemyManager.SpawnsLeft);
    }

    /// <summary>
    /// handleGoalReachedEnemy should be called from the GameOverChecker.
    /// </summary>
    private void handleGoalReachedEnemy(Enemy enemy)
    {
        currentLifes--;
        enemyManager.HandleGoalReachedEnemy(enemy);
        gameUI.UpdateLifes(currentLifes);

        if (currentLifes < 1)
        {
            CancelWave();
        }
    }
    public void OnCompleted() { }
    public void OnError(Exception error) { }
}
