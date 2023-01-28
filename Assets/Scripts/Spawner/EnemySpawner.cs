using System.Collections;
using UnityEngine;
using TMPro;


public class EnemySpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    [Header("Enemy Types")]
    public Transform towner;
    public Transform dorfschranze;
    public Transform boss;

    [Header("Attributes")]
    public float timeBetweenWaves = 5f;
    public float waveSpawnRate = 1f;
    public float countdown = 2f;
    public int waveNumber = 0;

    [Header("UI Text Fields")]
    public TMP_Text waveNumberText;
    public TMP_Text nextWaveCountdownText;

    private GameManager gameManager;
    private int maxWaveNumber;

    public StartAndSpeedupButton startAndSpeedupButton;

    // Singleton
    public static EnemySpawner instance;


    private void Start() {
        // Singleton
        if (instance == null) {
            instance = this;
        }

        enemiesAlive = 0;
        waveNumber = 0;

        gameManager = GameManager.instance;
        maxWaveNumber = gameManager.maxWaveNumber;

        waveNumberText.text = string.Format("Wave {0}/{1}", waveNumber, maxWaveNumber);
    }


    private void Update() {

        if (enemiesAlive > 0) {
            return;
        }

        if (waveNumber == maxWaveNumber) {
            gameManager.WinLevel();
            return;
        }

        startAndSpeedupButton.SetGameStateToBeforeNewRound();
    }


    public IEnumerator SpawnWave () {
        waveNumber++;
        enemiesAlive = waveNumber+5;
        gameManager.SetCurrentWaveNumber(waveNumber);

        waveNumberText.text = string.Format("Wave {0}/{1}", waveNumber, maxWaveNumber);

        for (int i = 0; i < waveNumber+5; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(waveSpawnRate);
        }
    }

    private void SpawnEnemy() {
        Instantiate(towner, transform.position, transform.rotation);
    }
}
