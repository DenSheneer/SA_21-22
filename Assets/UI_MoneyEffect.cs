using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MoneyEffect : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_moneyAdd;

    [SerializeField]
    float moneyEffectTime = 5.0f;
    float currentTime = 0.0f;
    bool effectActive = false;

    private void Awake()
    {
        text_moneyAdd.alpha = 0;
        
        effectActive = false;
        currentTime = moneyEffectTime;
    }

    public void AddMoney(uint addedMoney)
    {
        text_moneyAdd.alpha = 1.0f;
        text_moneyAdd.text = "+" + addedMoney;
        effectActive = true;
    }

    private void FixedUpdate()
    {
        if (effectActive)
        {
            transform.position += new Vector3(0, Time.deltaTime, 0);
            text_moneyAdd.alpha -= Time.deltaTime / moneyEffectTime;
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                effectActive = false;
                currentTime = moneyEffectTime;
            }
        }
    }
}
