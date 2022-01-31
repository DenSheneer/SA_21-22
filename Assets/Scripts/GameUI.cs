using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_money, text_lives;

    [SerializeField]
    UI_Building ui_building;

    [SerializeField]
    UI_Gameplay ui_gameplay;

    [SerializeField]
    Button button_quit;

    private void Awake()
    {
        if (ui_building != null)
        {
            ui_building.OnClose += openGameplayUI;
        }
        if (ui_gameplay != null)
        {
            ui_gameplay.OnClose += openBuildUI;
        }
    }

    void openGameplayUI()
    {
        ui_gameplay.Open();
    }
    void openBuildUI()
    {
        ui_building.Open();
    }
}
