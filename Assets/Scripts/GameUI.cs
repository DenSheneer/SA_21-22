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

    UserInterface current = null;

    private void Awake()
    {
        current = ui_building;
    }

    private void Start()
    {
        UnityGameOverChecker.Instance.OnLifesChange += UpdateLifes;
        UpdateLifes(UnityGameOverChecker.Instance.Lifes);
    }

    public void SetStartWaveButton(System.Action start)
    {
        ui_building.OnStartButtonClick += start;
    }
    public void SetResetWaveButton(System.Action reset)
    {
        ui_gameplay.OnResetButtonClick += reset;
    }

    public void OpenUI(UI_TYPE type)
    {
        switch (type)
        {
            case UI_TYPE.building:
                openUI(ui_building);
                current = ui_building;
                break;
            case UI_TYPE.gameplay:
                openUI(ui_gameplay);
                current = ui_gameplay;
                break;
        }
    }

    private void openUI(UserInterface nextUI)
    {
        current.Close();
        nextUI.Open();
    }

    public void UpdateMoney(uint money)
    {
        text_money.text = "Money: " + money.ToString();
    }
    public void UpdateLifes(uint lives)
    {
        text_lives.text = "Lives: " + lives.ToString();
    }
    public void quitGame()
    {
        Application.Quit();
    }
}

public enum UI_TYPE
{
    building = 0,
    gameplay = 1
}
