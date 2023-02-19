using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUIButtonHandler : MonoBehaviour {

    [Header("Level-Select Scene Name")]
    public string levelName = "LevelSelect";

    [Header("UI Elements")]
    public GameObject settingsUI;


    private void Start() {
        settingsUI.SetActive(false);
    }


    private void Update() {
        // hide Settings if ESC-Key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (settingsUI.activeSelf) {
                settingsUI.SetActive(false);
            }
        }
    }


    public void StartGame() {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }


    public void ShowSettingsUI() {
        settingsUI.SetActive(true);
    }

    public void HideSettingsUI() {
        settingsUI.SetActive(false);
    }
}
