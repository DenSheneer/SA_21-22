using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Permanent : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_money, text_lives;

    [SerializeField]
    Button button_quit;

    public Action OnResetButtonClick;

    private void Awake()
    {
        button_quit.onClick.AddListener(onResetButtonClick);
    }

    public void UpdateMoney(uint money)
    {
        text_money.text = "Money: " + money.ToString();
    }
    public void UpdateLifes(uint lives)
    {
        text_lives.text = "Lives: " + lives.ToString();
    }
    public void onResetButtonClick()
    {
        OnResetButtonClick?.Invoke();
    }
}
