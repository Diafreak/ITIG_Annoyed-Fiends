using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerUIButtonHandler : MonoBehaviour {

    [Header("Scenes")]
    public string menuSceneName = "MainMenu";

    [Header("UI Elements")]
    public GameObject pauseUI;
    public GameObject settingsUI;
    public GameObject tutorialUI;

    private float previousGameSpeed;

    private GameManager gameManager;
    private EnemySpawner enemySpawner;


    private void Start() {
        gameManager = GameManager.instance;
        enemySpawner = EnemySpawner.instance;
        pauseUI.SetActive(false);
        settingsUI.SetActive(false);
    }


    private void Update() {

        if (gameManager.IsGameOver()) {
            return;
        }

        if (gameManager.IsGameWon()) {
            return;
        }

        if (tutorialUI.activeSelf) {
            return;
        }

        // Check if ESC-Key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // if Settings are open, go back to PauseMenu
            if (settingsUI.activeSelf) {
                BackToPauseMenu();
            }
            else {
                TogglePauseMenuVisibility();
            }
        }
    }


    // Menu Button
    public void Menu() {
        SceneManager.LoadScene(menuSceneName);
        Time.timeScale = 1f;
    }

    // Retry Button
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    // LevelWonUI
    public void NextLevel() {
        SceneManager.LoadScene(gameManager.GetNextLevelName());
    }

    public void ContinueFreeplay() {
        gameManager.SetEndlessMode();
    }


    // PauseUI
    public void Continue() {
        TogglePauseMenuVisibility();
    }


    // SettingsUI
    public void BackToPauseMenu() {
        settingsUI.SetActive(false);
        pauseUI.SetActive(true);
    }


    public void TogglePauseMenuVisibility() {

        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            previousGameSpeed = Time.timeScale;
            Time.timeScale = 0f;
            gameManager.DisableEyeOfDoom();
        } else {
            Time.timeScale = previousGameSpeed;
            gameManager.EnableEyeOfDoom();
        }
    }


    // SettingsUI
    public void ShowSettingsUI() {
        settingsUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void HideSettingsUI() {
        settingsUI.SetActive(false);
        pauseUI.SetActive(true);
    }

}
