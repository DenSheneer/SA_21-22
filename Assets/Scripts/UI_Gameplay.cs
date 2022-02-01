using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Gameplay : MonoBehaviour, UserInterface
{
    [SerializeField]
    GameObject canvasObject;

    [SerializeField]
    TextMeshProUGUI text_kills, text_killsLeft;

    [SerializeField]
    Button button_reset;

    public Action OnResetButtonClick;

    private void Start()
    {
        button_reset.onClick.AddListener(onResetButtonClick);
        EnemySpawnHandler.Instance.OnEnemyDie += updateKills;
        EnemySpawnHandler.Instance.OnEnemySpawn += updateSpawns;
    }

    public void Open()
    {
        canvasObject.SetActive(true);
        updateSpawns(null);
        updateKills(null);
    }

    public void Close()
    {
        canvasObject.SetActive(false);
    }
    private void onResetButtonClick()
    {
        OnResetButtonClick?.Invoke();
    }
    private void updateKills(Enemy enemy)
    {
        text_kills.text = "Enemies killed: " + EnemySpawnHandler.Instance.Kills.ToString();
    }
    private void updateSpawns(Enemy enemy)
    {
        text_killsLeft.text = "Spawns left: " + EnemySpawnHandler.Instance.SpawnsLeft.ToString();
    }



}
