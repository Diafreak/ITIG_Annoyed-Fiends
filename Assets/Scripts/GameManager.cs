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
    public GameObject crosshairUI;


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
        UnlockMouse();
        PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
    }

    public void LostLevel() {
        Debug.Log("Game Over");
        gameOver = true;
        gameOverUI.SetActive(true);
        UnlockMouse();
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
            UnlockMouse();
        } else {
            Time.timeScale = previousGameSpeed;
            if (eyeWasPreviouslyActive) {
                LockMouse();
            }
        }
    }


    // ------------------------------
    // Mouse for Eye of Doom
    // ------------------------------

    private void UnlockMouse() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairUI.SetActive(false);
        eyeWasPreviouslyActive = switchGameMode.IsEyeOfDoomActive();
        switchGameMode.DisableEye();
    }

    private void LockMouse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshairUI.SetActive(true);
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
            LockMouse();
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