using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour {

    [Header("UI")]
    public GameObject ui;

    [Header("Texts")]
    public TMP_Text upgradeText;
    public TMP_Text sellText;

    [Header("Buttons")]
    public Button upgradeButton;
    public Button sellButton;

    private GridObject targetedGridObject;

    private GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
    }


    private void Update() {
        // disable or enable the Upgrade-Button depending if Player has enough Money
        if (ui.activeSelf && targetedGridObject != null) {
            if (PlayerHasEnoughMoney()) {
                upgradeButton.interactable = true;
            } else {
                upgradeButton.interactable = false;
            }
        }
    }

    public void SetTarget(GridObject gridObject) {
        // if the same tile is clicked again, hide the Upgrade/Sell-Menu
        if (targetedGridObject == gridObject && ui.activeSelf) {
            ui.SetActive(false);

        // show & move the Upgrade/Sell-Menu over the selected Tower
        } else {
            targetedGridObject = gridObject;
            transform.position = gridObject.GetWorldPosition();
            upgradeText.text = targetedGridObject.GetTower().GetUpgradeCost().ToString()  + "€";
            sellText.text    = targetedGridObject.GetTower().GetSellingPrice().ToString() + "€";
            ui.SetActive(true);
        }
    }

    public void Hide() {
        targetedGridObject = null;
        ui.SetActive(false);
    }

    public void Upgrade() {
        targetedGridObject.GetTower().UpgradeTower();
        upgradeText.text = targetedGridObject.GetTower().GetUpgradeCost().ToString()  + "€";
        sellText.text    = targetedGridObject.GetTower().GetSellingPrice().ToString() + "€";
    }

    public void Sell() {
        targetedGridObject.GetTower().SellTower();
        // clear Tower from the Grid-Array
        targetedGridObject.ClearPlacedTower();
        // reactivate the placement-tile at the sold towers position
        gridBuildingSystem.ReactivateGridTile(targetedGridObject.GetGridPosition().x, targetedGridObject.GetGridPosition().z);
        Hide();
    }

    private bool PlayerHasEnoughMoney() {
        return PlayerStats.GetMoney() >= targetedGridObject.GetTower().GetUpgradeCost();
    }
}
