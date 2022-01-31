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

    public Action OnClose;

    public void Start()
    {
        button_reset.onClick.AddListener(Close);
    }

    public void Open()
    {
        canvasObject.SetActive(true);
    }

    public void Close()
    {
        EnemySpawnHandler.Instance.ResetWave();
        canvasObject.SetActive(false);
        OnClose?.Invoke();
    }
}
