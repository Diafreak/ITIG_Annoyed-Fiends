using UnityEngine;
using TMPro;


public class TowerShop : MonoBehaviour {

    // all available Towers
    [Header("Towers")]
    public TowerTypeSO archerTowerSO;
    public TowerTypeSO devilTowerSO;
    public TowerTypeSO gargoyleTowerSO;

    [Header("Tower Textfields for Prices")]
    public TMP_Text archerTextField;
    public TMP_Text devilTextField;
    public TMP_Text gargoyleTextField;


    GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;

        archerTextField.text   = archerTowerSO.name   + " " + "$" + archerTowerSO.price;
        devilTextField.text    = devilTowerSO.name    + " " + "$" +  devilTowerSO.price;
        gargoyleTextField.text = gargoyleTowerSO.name + " " + "$" +  gargoyleTowerSO.price;
    }


    public void SelectArcher() {
        gridBuildingSystem.SetSelectedTower(archerTowerSO);
    }

    public void SelectDevil() {
        gridBuildingSystem.SetSelectedTower(devilTowerSO);
    }

    public void SelectGargoyle() {
        gridBuildingSystem.SetSelectedTower(gargoyleTowerSO);
    }
}
