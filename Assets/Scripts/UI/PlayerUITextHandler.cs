using UnityEngine;
using TMPro;


public class PlayerUITextHandler : MonoBehaviour {

    [Header("UI Texts")]
    public TMP_Text livesText;
    public TMP_Text moneyText;
    public TMP_Text waveNumberText;
    public TMP_Text gameOverWaveReachedText;

    // private EnemySpawner enemySpawner;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;


    private void Start() {
        gameManager  = GameManager.instance;
        enemySpawner = EnemySpawner.instance;
    }


    void Update() {
        livesText.text = "Lives: " + PlayerStats.lives.ToString();

        moneyText.text =  "$" + PlayerStats.GetMoney().ToString();

        if (enemySpawner.IsEndless()) {
            waveNumberText.text = string.Format("Wave {0}", gameManager.GetCurrentWaveNumber());
        } else {
            waveNumberText.text = string.Format("Wave {0}/{1}", gameManager.GetCurrentWaveNumber(), gameManager.GetMaxWaveNumber());
        }

        if (gameOverWaveReachedText.IsActive()) {
            gameOverWaveReachedText.text = "Wave Reached: " + gameManager.GetCurrentWaveNumber();
        }
    }
}
