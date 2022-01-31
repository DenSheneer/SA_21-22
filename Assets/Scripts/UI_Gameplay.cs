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

    private void Awake()
    {
        button_reset.onClick.AddListener(onResetButtonClick);
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
}
