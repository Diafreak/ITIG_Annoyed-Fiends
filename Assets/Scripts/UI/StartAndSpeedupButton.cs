using UnityEngine;
using TMPro;


public class StartAndSpeedupButton : MonoBehaviour {

    public enum GameState {
        beforeNewRound,
        play,
        speed2,
        speed3
    }

    public EnemySpawner enemySpawner;

    public TMP_Text buttonText;

    private static GameState state = GameState.beforeNewRound;

    // Singleton
    public static StartAndSpeedupButton instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }


    public void ChangeGameState() {

        switch (state) {
            case (GameState.beforeNewRound):
                Time.timeScale = 1f;
                state = GameState.play;
                StartCoroutine(enemySpawner.SpawnWave());
                buttonText.text = "1x Speed";
                break;

            case (GameState.play):
                Time.timeScale = 2f;
                state = GameState.speed2;
                buttonText.text = "x2 Speed";
                break;

            case (GameState.speed2):
                Time.timeScale = 3f;
                state = GameState.speed3;
                buttonText.text = "x3 Speed";
                break;

            case (GameState.speed3):
                Time.timeScale = 1f;
                state = GameState.play;
                buttonText.text = "1x Speed";
                break;
        }
    }


    public void SetGameStateToBeforeNewRound() {
        state = GameState.beforeNewRound;
        buttonText.text = "Start Round";
    }
}
