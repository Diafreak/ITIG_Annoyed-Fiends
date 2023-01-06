using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour {

    // all available Towers
    public TowerTypeSO archerTowerSO;
    public TowerTypeSO devilTowerSO;
    public TowerTypeSO gargoyleTowerSO;

    GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
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
