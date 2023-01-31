using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerUIButtonHandler : MonoBehaviour {

    [Header("Scenes")]
    public string nextLevelName;
    public string menuSceneName = "MainMenu";

    private GameManager gameManager;
    private EnemySpawner enemySpawner;


    private void Start() {
        gameManager = GameManager.instance;
        enemySpawner = EnemySpawner.instance;
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
        SceneManager.LoadScene(nextLevelName);
    }

    public void ContinueFreeplay() {
        gameManager.SetEndlessMode();
    }


    // PauseUI
    public void Continue() {
        gameManager.TogglePauseMenuVisibility();
    }
}
