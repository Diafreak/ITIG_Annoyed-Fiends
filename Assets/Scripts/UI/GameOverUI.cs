using UnityEngine;
using TMPro;


public class GameOverUI : MonoBehaviour {

    public TMP_Text waveReachedText;

    private GameManager gameManager;


    private void Start() {
        gameManager = GameManager.instance;
    }

    private void OnEnable() {
        waveReachedText.text = "Wave Reached: " + gameManager.GetCurrentWaveNumber();
    }
}
