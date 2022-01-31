using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Building : MonoBehaviour, UserInterface, IObserver<Transform>
{
    [SerializeField]
    GameObject canvasObject;

    [SerializeField]
    TextMeshProUGUI text_Strength, text_upgradePrice;

    [SerializeField]
    GameObject menu_buildMenu;

    [SerializeField]
    Button button_upgrade, button_startWave, button_closeUpgradeMenu;

    private MoneyManager moneyManag;
    private IDisposable unsubscriber;
    private Camera cam;

    private Transform selected;
    private Tower selectedTower;

    public Action OnClose;

    private void Start()
    {
        moneyManag = MoneyManager.Instance;
        cam = Camera.main;
        button_closeUpgradeMenu.onClick.AddListener(disableUpgradeMenu);
        button_startWave.onClick.AddListener(onStartButtonClick);

        Open();
    }

    void EnableUpgradeMenu(Vector3 screenPostion)
    {
        button_upgrade.gameObject.SetActive(true);
        menu_buildMenu.SetActive(true);
        menu_buildMenu.transform.position = screenPostion;
    }
    void disableUpgradeMenu()
    {
        button_upgrade.onClick.RemoveAllListeners();
        menu_buildMenu.SetActive(false);
    }

    public void OnCompleted()
    {
        unsubscriber.Dispose();
    }

    public void OnError(System.Exception error) { }

    public void OnNext(Transform value)
    {
        if (!menu_buildMenu.activeInHierarchy)
        {
            EnableUpgradeMenu(cam.WorldToScreenPoint(value.position));
            Tower tower = TowerManager.Instance.GetTowerByTransform(value);
            if (tower != null)
            {
                TowerProperties tp = tower.Properties;
                text_Strength.text = tp.Tier.ToString() + " tower";
                text_upgradePrice.text = "Build price: " + moneyManag.GetUpgradeCosts(tp.Tier);
                if (tp.Tier == TOWER_TIER.strong)
                {
                    text_upgradePrice.text = "This tower is already maxed out!";
                    button_upgrade.gameObject.SetActive(false);
                    return;
                }
            }
            else
            {
                text_Strength.text = "Nothing here yet!";
                text_upgradePrice.text = "Build price: " + moneyManag.GetUpgradeCosts(TOWER_TIER.none);
            }
            selected = value;
            selectedTower = tower;
            button_upgrade.onClick.AddListener(OnUpgradeButtonClick);
        }
    }

    void OnUpgradeButtonClick()
    {
        bool transactionpassed = false;
        if (selectedTower != null)
        {
            transactionpassed = moneyManag.RemoveMoney(moneyManag.GetUpgradeCosts(selectedTower.Properties.Tier));
        }else
        {
            transactionpassed = moneyManag.RemoveMoney(moneyManag.GetUpgradeCosts(TOWER_TIER.none));
        }
        if (transactionpassed)
        {
            TowerManager.Instance.BuildOrUpgrade(selected);
            selectedTower = null;
            selected = null;
            disableUpgradeMenu();
        }
    }
    void onStartButtonClick()
    {
        Close();
        EnemySpawnHandler.Instance.StartWave();
    }

    public void Open()
    {
        hookToSelector();
        canvasObject.SetActive(true);
    }

    public void Close()
    {
        disableUpgradeMenu();
        canvasObject.SetActive(false);
        OnClose?.Invoke();
    }

    private void hookToSelector()
    {
        unsubscriber = TowerManager.Instance.SusbscribeToSelector(this);
    }
}
