using UnityEngine;


public class GameManager : MonoBehaviour {

    public enum GameState {
        beforeNewRound,
        play,
        speed2,
        speed3
    }

    [Header("Win Condition")]
    public int maxWaveNumber = 5;

    [Header("Next Level Attributes")]
    public string nextLevelName;
    public int nextLevel;

    [Header("UI Elements")]
    public GameObject gameOverUI;
    public GameObject levelWonUI;
    public GameObject pauseUI;


    private bool gameOver = false;
    private int currentWaveNumber = 0;

    private float previousGameSpeed;
    private bool eyeWasPreviouslyActive;

    private SwitchGameMode switchGameMode;
    private EnemySpawner enemySpawner;

    // Singleton
    public static GameManager instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }

        gameOverUI.SetActive(false);
        levelWonUI.SetActive(false);
        pauseUI.SetActive(false);
    }


    private void Start() {
        switchGameMode = SwitchGameMode.instance;
        enemySpawner = EnemySpawner.instance;
    }


    void Update() {

        if (gameOver) {
            return;
        }

        if (PlayerStats.lives <= 0) {
            LostLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenuVisibility();
        }
    }


    // ------------------------------
    // Winning & Losing
    // ------------------------------

    public void WinLevel() {
        levelWonUI.SetActive(true);
        DisableEyeOfDoom();
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
    }

    public void LostLevel() {
        Debug.Log("Game Over");
        gameOver = true;
        gameOverUI.SetActive(true);
        DisableEyeOfDoom();
        Time.timeScale = 0f;
    }


    // ------------------------------
    // Pause Menu
    // ------------------------------

    public void TogglePauseMenuVisibility() {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf) {
            previousGameSpeed = Time.timeScale;
            Time.timeScale = 0f;
            DisableEyeOfDoom();
        } else {
            Time.timeScale = previousGameSpeed;
            if (eyeWasPreviouslyActive) {
                EnableEyeOfDoom();
            }
        }
    }


    // ------------------------------
    // Eye of Doom
    // ------------------------------

    private void DisableEyeOfDoom() {
        eyeWasPreviouslyActive = switchGameMode.IsEyeOfDoomActive();
        switchGameMode.DisableEye();
    }

    private void EnableEyeOfDoom() {
        switchGameMode.EnableEye();
    }


    // ------------------------------
    // Endless
    // ------------------------------
    public void SetEndlessMode() {
        enemySpawner.SetEndlessMode();
        ContinueGameAfterWin();
    }

    private void ContinueGameAfterWin() {
        levelWonUI.SetActive(false);
        if (eyeWasPreviouslyActive) {
            EnableEyeOfDoom();
        }
    }


    // ------------------------------
    // Getter & Setter
    // ------------------------------

    public void SetCurrentWaveNumber(int waveNumber) {
        currentWaveNumber = waveNumber;
    }

    public int GetCurrentWaveNumber() {
        return currentWaveNumber;
    }

    public int GetMaxWaveNumber() {
        return maxWaveNumber;
    }

    public string GetNextLevelName() {
        return nextLevelName;
    }
}