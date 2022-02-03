using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Building : MonoBehaviour, UserInterface
{
    [SerializeField]
    GameObject canvasObject;

    [SerializeField]
    TextMeshProUGUI text_Strength, text_upgradePrice;

    [SerializeField]
    GameObject menu_buildMenu;

    [SerializeField]
    Button button_upgrade, button_startWave, button_closeUpgradeMenu;

    private IDisposable unsubscriber;

    private Transform selected;
    private Tower selectedTower;

    public Action OnStartButtonClick;
    public Action OnUpgradeButtonClick;

    private void Awake()
    {
        button_upgrade.onClick.AddListener(onUpgradeButtonClick);
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

    public void SelectTower(Transform selectedSpace, Tower tower, uint emptyBuildCosts)
    {
        if (!menu_buildMenu.activeInHierarchy)
        {
            OpenUpgradeMenu(Camera.main.WorldToScreenPoint(selectedSpace.position));
            if (tower != null)
            {
                TowerProperties tp = tower.Properties;
                text_Strength.text = tower.TierName + " tower";
                if (tower.IsMaxTier())
                {
                    text_upgradePrice.text = "Tower is already maxed out!";
                    button_upgrade.gameObject.SetActive(false);
                    return;
                }
                text_upgradePrice.text = "Build price: " + tower.UpgradePath.NextPowerTier(tower.Tier).costs;
            }
            else
            {
                text_Strength.text = "Nothing here yet!";
                text_upgradePrice.text = "Build price: " + emptyBuildCosts;
            }
            selectedTower = tower;
            selected = selectedSpace;
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

    public void Open()
    {
        canvasObject.SetActive(true);
    }

    public void Close()
    {
        CloseUpgradeMenu();
        canvasObject.SetActive(false);
    }
}
