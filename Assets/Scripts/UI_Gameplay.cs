using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Gameplay : MonoBehaviour, iUserInterface
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
        //EnemySpawnHandler.Instance.OnEnemyDie += updateKills;
        //EnemySpawnHandler.Instance.OnEnemySpawn += updateSpawns;
    }

    public void Open()
    {
        canvasObject.SetActive(true);
    }

    public void Close()
    {
        canvasObject.SetActive(false);
    }
    private void onResetButtonClick()
    {
        OnResetButtonClick?.Invoke();
    }
    public void UpdateKills(uint kills)
    {
        text_kills.text = "Enemies killed: " + kills;
    }
    public void UpdateSpawns(int spawnsLeft)
    {
        text_killsLeft.text = "Spawns left: " + spawnsLeft;
    }



}
