using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Building : MonoBehaviour, UserInterface
{
    [SerializeField] GameObject canvasObject;
    [SerializeField] TextMeshProUGUI text_Strength, text_upgradePrice, text_poisonPrice;
    [SerializeField] GameObject menu_buildMenu;
    [SerializeField] Button button_upgrade, button_poison, button_startWave, button_closeUpgradeMenu;

    public Action OnStartButtonClick;
    public Action OnUpgradeButtonClick;
    public Action OnPoisonButtonClick;

    private void Awake()
    {
        button_upgrade.onClick.AddListener(onUpgradeButtonClick);
        button_poison.onClick.AddListener(onPoisonButtonClick);
        button_closeUpgradeMenu.onClick.AddListener(CloseUpgradeMenu);
        button_startWave.onClick.AddListener(onStartButtonClick);
    }

    void OpenUpgradeMenu(Vector3 screenPostion)
    {
        button_upgrade.gameObject.SetActive(true);
        menu_buildMenu.SetActive(true);
        menu_buildMenu.transform.position = screenPostion;
    }
    public void CloseUpgradeMenu()
    {
        menu_buildMenu.SetActive(false);
    }

    public void HandleSpaceSelect(Transform selectedSpace, Tower tower, uint emptyBuildCosts)
    {
        if (!menu_buildMenu.activeInHierarchy)
        {
            OpenUpgradeMenu(Camera.main.WorldToScreenPoint(selectedSpace.position));
            if (tower != null)
            {
                fillInTextForPowerUpgrade(tower);
                fillInTextForPoisonUpgrade(tower);
            }
            else
            {
                fillInTextForEmptySpace(emptyBuildCosts);
            }
        }
    }

    public void Open()
    {
        canvasObject.SetActive(true);
    }

    public void Close()
    {
        CloseUpgradeMenu();
        canvasObject.SetActive(false);
    }
    void fillInTextForEmptySpace(uint emptyBuildCosts)
    {
        button_poison.gameObject.SetActive(false);
        text_poisonPrice.gameObject.SetActive(false);
        text_Strength.text = "Nothing here yet!";
        text_upgradePrice.text = "Build price: " + emptyBuildCosts;
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
}
