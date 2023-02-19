using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TowerUI : MonoBehaviour {

    [Header("UI")]
    public GameObject screenSpaceUI;
    public GameObject worldSpaceUI;
    public Transform towerRange;

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
        HideUI();
    }


    private void Update() {
        // disable or enable the Upgrade-Button depending if Player has enough Money
        if (worldSpaceUI.activeSelf && targetedGridObject != null) {
            if (PlayerHasEnoughMoney()) {
                upgradeButton.interactable = true;
            } else {
                upgradeButton.interactable = false;
            }
        }
    }


    // sets the selected Tower
    public void SetTarget(GridObject gridObject) {

        if (targetedGridObject == gridObject && worldSpaceUI.activeSelf) {
            // if the same tile is clicked again, hide the Upgrade/Sell-Menu
            HideUI();
        } else {
            // show & move the Upgrade/Sell-Menu over the selected Tower
            targetedGridObject = gridObject;
            worldSpaceUI.transform.position = gridObject.GetWorldPosition();
            UpdateUI();
            worldSpaceUI.SetActive(true);
            screenSpaceUI.SetActive(true);
        }
    }


    private void UpdateUI() {
        upgradeText.text = "$" + targetedGridObject.GetTower().GetUpgradeCost().ToString();
        sellText.text    = "$" + targetedGridObject.GetTower().GetSellingPrice().ToString();
        ShowTowerRange();
    }


    public void HideUI() {
        HideTowerRange();
        targetedGridObject = null;
        worldSpaceUI.SetActive(false);
        screenSpaceUI.SetActive(false);
    }



    // ------------------------------
    // Buttons
    // ------------------------------

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
        HideUI();
    }

    private bool PlayerHasEnoughMoney() {
        return PlayerStats.GetMoney() >= targetedGridObject.GetTower().GetUpgradeCost();
    }



    // ------------------------------
    // Tower-Range
    // ------------------------------

    public void ShowTowerRange() {
        towerRange.position = targetedGridObject.GetWorldPosition() + gridBuildingSystem.GetBuildOffset();
        float range = targetedGridObject.GetTower().GetRange();
        towerRange.localScale = new Vector3(range/4, range/4);
        towerRange.gameObject.SetActive(true);
    }

    public void HideTowerRange() {
        towerRange.gameObject.SetActive(false);
    }
}
