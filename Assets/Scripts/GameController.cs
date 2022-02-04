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
        setupMoneyManager_Action();
        setupUI_Actions();
        setupEnemyManager_Actions();
    }

    private void Start()
    {
        unsubscriber = selector.Subscribe(this);

        startBuildPhase();
    }
    private void startWave()
    {
        saveOldValues();
        gameUI.OpenUI(UI_TYPE.gameplay);
        enemyManager.StartWave();
    }
    void cancelWave()
    {
        restoreOldValues();
        enemyManager.ResetWave();
        startBuildPhase();
    }

    void startBuildPhase()
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
        build_UI.HandleSpaceSelect(selectedSpace, selectedTower, towerManager.GetFirstBuildCosts());
    }
    private void tryUpgrade()
    {
        bool transactionPassed = false;
        if (selectedTower != null)
        {
            transactionPassed = moneyManager.RemoveMoney(selectedTower.UpgradePath.NextPowerTier(selectedTower.Tier).costs);
        }
        else
        {
            transactionPassed = moneyManager.RemoveMoney(towerManager.GetFirstBuildCosts());
        }
        if (transactionPassed)
        {
            towerManager.BuildOrUpgrade(selectedSpace);
            build_UI.CloseUpgradeMenu();
        }
    }
    private void tryAddPoison()
    {
        bool transactionPassed = false;
        if (selectedTower != null)
        {
            transactionPassed = moneyManager.RemoveMoney(selectedTower.UpgradePath.GetAddPoisonCosts());
        }
        if (transactionPassed)
        {
            selectedTower.PoisonEnabled = true;
            build_UI.CloseUpgradeMenu();
        }
    }
    private void tryAddAOE()
    {
        bool transactionPassed = false;
        if (selectedTower != null)
        {
            transactionPassed = moneyManager.RemoveMoney(selectedTower.UpgradePath.GetAddPoisonCosts());
        }
        if (transactionPassed)
        {
            selectedTower.AOE_Enabled = true;
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
            cancelWave();
        }
    }
    private void setupEnemyManager_Actions()
    {
        enemyManager.OnWaveComplete += startBuildPhase;
        enemyManager.OnComplete += gameWon;
        enemyManager.OnEnemyKill += handleEnemyKilled;
        enemyManager.OnEnemySpawn += handleEnemySpawned;
    }
    private void setupUI_Actions()
    {
        gameUI.OnResetButtonClick += closeGame;
        gameOverChecker.OnEnemyGoalReach += handleGoalReachedEnemy;
        gameplay_UI.OnResetButtonClick += cancelWave;
        build_UI.OnUpgradeButtonClick += tryUpgrade;
        build_UI.OnPoisonButtonClick += tryAddPoison;
        build_UI.OnAOE_ButtonClick += tryAddAOE;
        build_UI.OnStartButtonClick += startWave;
    }
    private void setupMoneyManager_Action()
    {
        moneyManager.OnMoneyChange += gameUI.UpdateMoney;
    }
    void saveOldValues()
    {
        moneyBeforeReset = moneyManager.Money;
        killsbeforeReset = currentKills;
        lifesBeforeReset = currentLifes;
    }
    void restoreOldValues()
    {
        moneyManager.SetMoney(moneyBeforeReset);
        currentLifes = lifesBeforeReset;
        currentKills = killsbeforeReset;
    }
    void closeGame()
    {
        Application.Quit();
    }
    public void OnCompleted() { }
    public void OnError(Exception error) { }
}
