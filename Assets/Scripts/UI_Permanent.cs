using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Permanent : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_waveNr, text_money, text_lives;

    [SerializeField]
    public UI_MoneyEffect moneyEffect;

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

    public void UpdateLifes(uint lifes)
    {
        text_lives.text = "Lives: " + lifes.ToString();
    }
    public void UpdateWaveNr(int waveNr)
    {
        text_waveNr.text = "Wave: " + waveNr;
    }
    public void onResetButtonClick()
    {
        OnResetButtonClick?.Invoke();
    }
}
