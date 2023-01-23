using UnityEngine;
using TMPro;


public class GameOverUI : MonoBehaviour {

    public TMP_Text waveReachedText;

    private GameManager gameManager;


    private void OnEnable() {
        gameManager = GameManager.instance;
        waveReachedText.text = "Wave Reached: " + gameManager.GetCurrentWaveNumber();
    }
}
