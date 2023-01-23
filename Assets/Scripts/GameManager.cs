using UnityEngine;


public class GameManager : MonoBehaviour {

    [Header("Win Condition")]
    public int maxWaveNumber = 5;

    [Header("Next Level Attributes")]
    public string nextLevelName;
    public int nextLevel;

    [Header("UI Elements")]
    public GameObject gameOverUI;
    public GameObject levelWonUI;
    public PauseMenuUI pauseMenuUI;


    private bool gameOver = false;
    private int currentWaveNumber = 0;


    // Singleton
    public static GameManager instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }

        gameOverUI.SetActive(false);
        levelWonUI.SetActive(false);
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
        Debug.Log("Game Over");
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void SetCurrentWaveNumber(int waveNumber) {
        currentWaveNumber = waveNumber;
    }

    public int GetCurrentWaveNumber() {
        return currentWaveNumber;
    }
}
