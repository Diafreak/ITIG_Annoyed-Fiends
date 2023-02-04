using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TowerUI : MonoBehaviour {

    [Header("UI")]
    public GameObject towerUI;

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
        if (towerUI.activeSelf && targetedGridObject != null) {
            if (PlayerHasEnoughMoney()) {
                upgradeButton.interactable = true;
            } else {
                upgradeButton.interactable = false;
            }
        }
    }


    public void SetTarget(GridObject gridObject) {

        if (targetedGridObject == gridObject && towerUI.activeSelf) {
            // if the same tile is clicked again, hide the Upgrade/Sell-Menu
            Hide();
        } else {
            // show & move the Upgrade/Sell-Menu over the selected Tower
            targetedGridObject = gridObject;
            transform.position = gridObject.GetWorldPosition();
            UpdateUI();
            towerUI.SetActive(true);
        }
    }


    public void Upgrade() {
        targetedGridObject.GetTower().UpgradeTower();
        UpdateUI();
    }


    public void Sell() {
        targetedGridObject.GetTower().SellTower();
        // clear Tower from the Grid-Array
        targetedGridObject.ClearPlacedTower();
        // reactivate the placement-tile at the sold towers position
        gridBuildingSystem.ReactivateGridTile(targetedGridObject.GetGridPosition().x, targetedGridObject.GetGridPosition().z);
        Hide();
    }


    private void UpdateUI() {
        upgradeText.text = "$" + targetedGridObject.GetTower().GetUpgradeCost().ToString();
        sellText.text    = "$" + targetedGridObject.GetTower().GetSellingPrice().ToString();
        targetedGridObject.GetTower().ShowTowerRange(targetedGridObject);
    }


    public void Hide() {
        targetedGridObject?.GetTower()?.HideRange();
        targetedGridObject = null;
        towerUI.SetActive(false);
    }


    private bool PlayerHasEnoughMoney() {
        return PlayerStats.GetMoney() >= targetedGridObject.GetTower().GetUpgradeCost();
    }
}
