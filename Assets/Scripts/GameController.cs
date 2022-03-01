using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour, IObserver<Transform>
{
    [SerializeField]
    float timeBetweenWaves = 30.0f;

    Unitytimer waveTimer;

    uint moneyBeforeReset = 0;

    uint currentKills = 0;
    uint killsbeforeReset = 0;

    uint currentLifes = 10;
    uint lifesBeforeReset = 0;

    iUserInterface ui_current = null;
    iTowerSelector selector;
    Transform selectedSpace;
    Tower selectedTower;

    [SerializeField] TowerManager towerManager;
    [SerializeField] EnemySpawnHandler enemyManager;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] UnityGameOverChecker gameOverChecker;
    [SerializeField] UI_Permanent ui_permanent;
    [SerializeField] UI_Building ui_building;
    [SerializeField] UI_Gameplay ui_gameplay;

    System.IDisposable unsubscriber;

    private void Awake()
    {
        selector = GetComponent<iTowerSelector>();
        waveTimer = GetComponent<Unitytimer>();
        waveTimer.Initialize(timeBetweenWaves, startWave, false);

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
        switchToUI(UI_TYPE.gameplay);
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
        waveTimer.ResetTimer();
        waveTimer.IsPaused = false;
        switchToUI(UI_TYPE.building);
        ui_gameplay.UpdateKills(currentKills);
        ui_permanent.UpdateLifes(currentLifes);
        ui_permanent.UpdateWaveNumber(enemyManager.GetWaveNumber());
        ui_permanent.UpdateMoney(moneyManager.Money);
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
        ui_building.HandleSpaceSelect(selectedSpace, selectedTower, towerManager.GetFirstBuildCosts());
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
        }
        ui_handleTransaction(transactionPassed);
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
        }
        ui_handleTransaction(transactionPassed);
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
        }
        ui_handleTransaction(transactionPassed);

    }
    private void switchToUI(UI_TYPE type)
    {
        switch (type)
        {
            case UI_TYPE.building:
                openUI(ui_building);
                ui_current = ui_building;
                break;
            case UI_TYPE.gameplay:
                openUI(ui_gameplay);
                ui_current = ui_gameplay;
                break;
        }
    }
    private void openUI(iUserInterface nextUI)
    {
        if (ui_current != null)
        {
            ui_current.Close();
        }
        nextUI.Open();
    }
    private void ui_handleTransaction(bool transactionPassed)
    {
        if (transactionPassed)
        {
            ui_building.HandlePassedPurchase();
        }
        else
        {
            ui_building.HandleFailedPurchase();
        }
    }

    /// <summary>
    /// handleEnemyKilled should be called from an enemy that was killed by the player.
    /// </summary>
    private void handleEnemyKilled(Enemy enemy)
    {
        currentKills++;
        moneyManager.AddMoney(enemy.GetMoney);
        ui_gameplay.UpdateKills(currentKills);
        enemyManager.HandleKilledEnemy(enemy);
    }
    /// <summary>
    /// handleEnemySpawned should be called from the EnemySpawnHandler.
    /// </summary>
    private void handleEnemySpawned(Enemy enemy)
    {
        ui_gameplay.UpdateSpawns(enemyManager.SpawnsLeft);
    }

    /// <summary>
    /// handleGoalReachedEnemy should be called from the GameOverChecker.
    /// </summary>
    private void handleGoalReachedEnemy(Enemy enemy)
    {
        currentLifes--;
        enemyManager.HandleGoalReachedEnemy(enemy);
        ui_permanent.UpdateLifes(currentLifes);

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
        ui_permanent.OnResetButtonClick += closeGame;
        gameOverChecker.OnEnemyGoalReach += handleGoalReachedEnemy;
        ui_gameplay.OnResetButtonClick += cancelWave;
        ui_building.OnUpgradeButtonClick += tryUpgrade;
        ui_building.OnPoisonButtonClick += tryAddPoison;
        ui_building.OnAOE_ButtonClick += tryAddAOE;
        ui_building.OnStartButtonClick += startWave;
    }
    private void setupMoneyManager_Action()
    {
        moneyManager.OnMoneyChange += ui_permanent.UpdateMoney;
        moneyManager.OnMoneyAdd += ui_permanent.moneyEffect.AddMoney;
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
    void Update()
    {
        ui_building.UpdateTimeLeft((uint)waveTimer.GetTimeLeft());
    }
    public void OnCompleted() { }
    public void OnError(Exception error) { }
}
public enum UI_TYPE
{
    building = 0,
    gameplay = 1
}
