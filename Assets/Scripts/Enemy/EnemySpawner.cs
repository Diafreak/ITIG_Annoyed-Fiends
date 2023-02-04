using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    [Header("Enemy Types")]
    public EnemyTypeSO townerSO;
    public EnemyTypeSO dorfschranzeSO;
    public EnemyTypeSO holzfaellerSO;

    [Header("Attributes")]
    public float timeBetweenWaves = 5f;
    public float waveSpawnRate = 1f;
    public float countdown = 2f;
    public int waveNumber;

    private int maxWaveNumber;
    private bool isEndless;

    private StartAndSpeedupButton startAndSpeedupButton;
    private GameManager gameManager;

    // Singleton
    public static EnemySpawner instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }


    private void Start() {
        enemiesAlive = 0;
        waveNumber = 0;
        isEndless = false;

        gameManager = GameManager.instance;
        startAndSpeedupButton = StartAndSpeedupButton.instance;
        maxWaveNumber = gameManager.GetMaxWaveNumber();
    }


    private void Update() {

        if (enemiesAlive > 0) {
            return;
        }

        if (!isEndless && waveNumber == maxWaveNumber) {
            gameManager.WinLevel();
            return;
        }

        startAndSpeedupButton.SetGameStateToBeforeNewRound();
    }


    public IEnumerator SpawnWave () {
        waveNumber++;
        enemiesAlive = waveNumber+5;
        gameManager.SetCurrentWaveNumber(waveNumber);

        for (int i = 0; i < waveNumber+5; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(waveSpawnRate);
        }
    }


    private void SpawnEnemy() {
        Enemy.Create(transform.position, townerSO);
    }


    public int GetCurrentWaveNumber() {
        return waveNumber;
    }


    public void SetEndlessMode() {
        isEndless = true;
    }

    public bool IsEndless() {
        return isEndless;
    }
}
