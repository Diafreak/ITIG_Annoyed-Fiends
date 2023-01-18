using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    private bool gameOver = false;
    public string nextLevelName = "Level2";
    public int    nextLevel     = 2;

    public GameObject gameOverUI;
    public PauseMenuUI pauseMenuUI;
    public GameObject levelWonUI;

    // Singleton
    public static GameManager instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        if (gameOverUI.activeSelf) {
            gameOverUI.SetActive(false);
        }
    }


    void Update() {

        if (gameOver) {
            return;
        }

        if (PlayerStats.lives <= 0) {
            LostLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenuUI.ToggleVisible();
        }
    }


    public void WinLevel() {
        levelWonUI.SetActive(true);
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
    }

    public void LostLevel() {
        gameOver = true;
        gameOverUI.SetActive(true);
    }
}
