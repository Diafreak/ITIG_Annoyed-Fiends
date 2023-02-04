using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TowerShop : MonoBehaviour {

    // all available Towers
    [Header("TowerSO's")]
    public TowerTypeSO archerTowerSO;
    public TowerTypeSO devilTowerSO;
    public TowerTypeSO gargoyleTowerSO;

    [Header("Tower Textfields for Prices")]
    public TMP_Text archerTextField;
    public TMP_Text devilTextField;
    public TMP_Text gargoyleTextField;

    [Header("Gargoyle")]
    public Button gargoyleButton;
    public float gargoyleCooldown;
    public TMP_Text gargoyleCooldownText;

    private float timeLeftUntilGargoyleUnlocked;
    private bool gargoyleIsPlaced;

    private GridBuildingSystem gridBuildingSystem;

    // Singleton
    public static TowerShop instance;



    private void Start() {
        // Singleton
        if (instance == null) {
            instance = this;
        }

        gridBuildingSystem = GridBuildingSystem.instance;

        archerTextField.text   = archerTowerSO.name   + " " + "$" + archerTowerSO.price;
        devilTextField.text    = devilTowerSO.name    + " " + "$" + devilTowerSO.price;
        gargoyleTextField.text = gargoyleTowerSO.name + " " + "$" + gargoyleTowerSO.price;

        timeLeftUntilGargoyleUnlocked = 0f;
        UnlockGargoyle();
        gargoyleCooldownText.text = string.Format("{0:0}", gargoyleCooldown);
    }


    private void Update() {

        if (!gargoyleIsPlaced) {
            return;
        }

        if (timeLeftUntilGargoyleUnlocked <= 0f) {
            UnlockGargoyle();
            return;
        }

        timeLeftUntilGargoyleUnlocked -= Time.deltaTime;
        UpdateGargoyleCooldownText();
    }


    // ------------------------------
    // Button Selection
    // ------------------------------

    public void SelectArcher() {
        gridBuildingSystem.SetSelectedTower(archerTowerSO);
    }

    public void SelectDevil() {
        gridBuildingSystem.SetSelectedTower(devilTowerSO);
    }

    public void SelectGargoyle() {
        gridBuildingSystem.SetSelectedTower(gargoyleTowerSO);
    }



    // ------------------------------
    // Gargoyle-Timer
    // ------------------------------

    public void LockGargoyle() {

        if (gargoyleIsPlaced) {
            return;
        }

        gargoyleIsPlaced = true;
        gargoyleButton.interactable = false;
        gargoyleCooldownText.gameObject.SetActive(true);
        timeLeftUntilGargoyleUnlocked = gargoyleCooldown;
    }


    private void UnlockGargoyle() {
        gargoyleIsPlaced = false;
        gargoyleButton.interactable = true;
        gargoyleCooldownText.gameObject.SetActive(false);
    }


    private void UpdateGargoyleCooldownText() {
        gargoyleCooldownText.text = string.Format("{0:0}", timeLeftUntilGargoyleUnlocked);
    }
}
