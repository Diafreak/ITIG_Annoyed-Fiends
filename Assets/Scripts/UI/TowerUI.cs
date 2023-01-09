using UnityEngine;


public class TowerUI : MonoBehaviour {

    public GameObject ui;

    private GridObject gridObject;

    GridBuildingSystem gridBuildingSystem;


    /*private void Awake() {
        gridBuildingSystem = GridBuildingSystem.instance;
    }*/


    public void SetTarget(GridObject givenGridObject) {
        // if the same tile is clicked again, hide the Upgrade/Sell-Menu
        Vector3 gridPosition = givenGridObject.GetWorldPosition();

        if (gridObject == givenGridObject && ui.activeSelf) {
            ui.SetActive(false); Debug.Log("active false");

        // show & move the Upgrade/Sell-Menu over the selected Tower
        } else {
            transform.position = gridPosition;
            gridObject = givenGridObject;
            ui.SetActive(true);
        }
    }

    public void Hide() {
        ui.SetActive(false);
    }

    public void Upgrade() {
        gridObject.GetPlacedTower().UpgradeTower();
    }
}
