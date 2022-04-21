using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Building : MonoBehaviour, iUserInterface
{
    [SerializeField] GameObject canvasObject;
    [SerializeField] TextMeshProUGUI text_waveTimeLeft, text_Strength, text_upgradePrice, text_poisonPrice, text_aoePrice;
    [SerializeField] GameObject menu_buildMenu;
    [SerializeField] Button button_upgrade, button_poison, button_aoe, button_startWave, button_closeUpgradeMenu;

    public Action OnStartButtonClick;
    public Action OnUpgradeButtonClick;
    public Action OnPoisonButtonClick;
    public Action OnAOE_ButtonClick;

    private void Awake()
    {
        button_upgrade.onClick.AddListener(onUpgradeButtonClick);
        button_poison.onClick.AddListener(onPoisonButtonClick);
        button_aoe.onClick.AddListener(onAOE_ButtonClick);
        button_closeUpgradeMenu.onClick.AddListener(closeUpgradeMenu);
        button_startWave.onClick.AddListener(onStartButtonClick);
    }

    public void UpdateTimeLeft(uint timeLeft)
    {
        text_waveTimeLeft.text = "Time before wave: : " + timeLeft;
    }

    void openUpgradeMenu(Vector3 screenPostion)
    {
        button_upgrade.gameObject.SetActive(true);
        menu_buildMenu.SetActive(true);
        menu_buildMenu.transform.position = screenPostion;
    }

    public void HandlePassedPurchase()
    {
        closeUpgradeMenu();
    }
    public void HandleFailedPurchase()
    {
        return;
    }

    private void closeUpgradeMenu()
    {
        menu_buildMenu.SetActive(false);
    }

    public void HandleSpaceSelect(Tower tower)
    {
        if (!menu_buildMenu.activeInHierarchy)
        {
            openUpgradeMenu(Camera.main.WorldToScreenPoint(tower.transform.position));
            if (tower.UpgradePath != null)
            {
                if (tower.Tier > 0)
                {
                    fillInTextForPowerUpgrade(tower);
                    fillInTextForAOE_Upgrade(tower);
                    fillInTextForPoisonUpgrade(tower);
                }
                else
                {
                    fillInTextForEmptySpace(tower);
                }
            }
        }
    }

    public void Open()
    {
        canvasObject.SetActive(true);
        closeUpgradeMenu();
    }

    public void Close()
    {
        closeUpgradeMenu();
        canvasObject.SetActive(false);
    }
    void fillInTextForEmptySpace(Tower emptyTowerSpace)
    {
        button_aoe.gameObject.SetActive(false);
        text_aoePrice.gameObject.SetActive(false);
        button_poison.gameObject.SetActive(false);
        text_poisonPrice.gameObject.SetActive(false);
        text_Strength.text = "Nothing here yet!";
        text_upgradePrice.text = "Build price: " + emptyTowerSpace.UpgradePath.NextPowerTier(emptyTowerSpace.Tier).costs;
    }

    void fillInTextForAOE_Upgrade(Tower tower)
    {
        if (!tower.AOE_Enabled)
        {
            button_aoe.gameObject.SetActive(true);
            text_aoePrice.gameObject.SetActive(true);
            text_aoePrice.text = "Price: " + tower.UpgradePath.GetAddAOE_Costs();
        }
        else
        {
            text_aoePrice.gameObject.SetActive(true);
            button_aoe.gameObject.SetActive(false);
            text_aoePrice.text = "Already purchased!";
        }
    }

    void fillInTextForPoisonUpgrade(Tower tower)
    {
        if (!tower.PoisonEnabled)
        {
            button_poison.gameObject.SetActive(true);
            text_poisonPrice.gameObject.SetActive(true);
            text_poisonPrice.text = "Price: " + tower.UpgradePath.GetAddPoisonCosts();
        }
        else
        {
            text_poisonPrice.gameObject.SetActive(true);
            button_poison.gameObject.SetActive(false);
            text_poisonPrice.text = "Already purchased!";
        }
    }
    void fillInTextForPowerUpgrade(Tower tower)
    {
        text_Strength.text = tower.TierName + " tower";
        if (tower.IsMaxTier())
        {
            text_upgradePrice.text = "Maxed out!";
            button_upgrade.gameObject.SetActive(false);
            return;
        }
        else
        {
            text_upgradePrice.text = "Price: " + tower.UpgradePath.NextPowerTier(tower.Tier).costs;
        }
    }
    void onUpgradeButtonClick()
    {
        OnUpgradeButtonClick?.Invoke();
    }
    void onStartButtonClick()
    {
        OnStartButtonClick?.Invoke();
    }
    void onPoisonButtonClick()
    {
        OnPoisonButtonClick?.Invoke();
    }
    void onAOE_ButtonClick()
    {
        OnAOE_ButtonClick?.Invoke();
    }
}
