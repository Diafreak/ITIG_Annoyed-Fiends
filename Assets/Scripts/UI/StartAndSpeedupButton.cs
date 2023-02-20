using UnityEngine;
using TMPro;


public class StartAndSpeedupButton : MonoBehaviour {

    public TMP_Text buttonText;

    private static GameManager.GameState state;

    private EnemySpawner enemySpawner;

    // Singleton
    public static StartAndSpeedupButton instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        enemySpawner = EnemySpawner.instance;
        state = GameManager.GameState.beforeNewRound;
    }


    public void ChangeGameState() {

        switch (state) {
            case (GameManager.GameState.beforeNewRound):
                Time.timeScale = 1f;
                state = GameManager.GameState.play;
                StartCoroutine(enemySpawner.SpawnWave());
                buttonText.text = "x1 Speed";
                break;

            case (GameManager.GameState.play):
                Time.timeScale = 2f;
                state = GameManager.GameState.speed2;
                buttonText.text = "x2 Speed";
                break;

            case (GameManager.GameState.speed2):
                Time.timeScale = 3f;
                state = GameManager.GameState.speed3;
                buttonText.text = "x3 Speed";
                break;

            case (GameManager.GameState.speed3):
                Time.timeScale = 1f;
                state = GameManager.GameState.play;
                buttonText.text = "x1 Speed";
                break;
        }
    }


    public void SetGameStateToBeforeNewRound() {
        Time.timeScale = 1f;
        state = GameManager.GameState.beforeNewRound;
        buttonText.text = "Start Round";
    }
}
