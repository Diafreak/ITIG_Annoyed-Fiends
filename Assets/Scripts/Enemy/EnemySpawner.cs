using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    [Header("Enemy Types")]
    public EnemyTypeSO farmerSO;
    public EnemyTypeSO dorfschranzeSO;
    public EnemyTypeSO holzfaellerSO;

    [Header("Attributes")]
    public float waveSpawnRate = 1f;

    public int minWaveNumberForDorfschranze = 3;
    public int minWaveNumberForHolzfaeller = 8;

    private int currentWaveNumber;
    private int maxWaveNumber;
    private bool isEndless;

    private int waveMultiplicator;

    private int numberOfEnemyTypes;

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
        currentWaveNumber = 0;
        isEndless = false;

        waveMultiplicator = 10;

        numberOfEnemyTypes = 1;

        gameManager = GameManager.instance;
        startAndSpeedupButton = StartAndSpeedupButton.instance;
        maxWaveNumber = gameManager.GetMaxWaveNumber();
    }


    private void Update() {

        if (enemiesAlive > 0) {
            return;
        }

        if (!isEndless && currentWaveNumber == maxWaveNumber) {
            gameManager.WinLevel();
            return;
        }

        startAndSpeedupButton.SetGameStateToBeforeNewRound();
    }


    public IEnumerator SpawnWave () {
        currentWaveNumber++;
        enemiesAlive = currentWaveNumber * waveMultiplicator;
        gameManager.SetCurrentWaveNumber(currentWaveNumber);

        for (int i = 0; i < currentWaveNumber * waveMultiplicator; i++) {

            if (currentWaveNumber > minWaveNumberForDorfschranze && currentWaveNumber <= minWaveNumberForHolzfaeller) {
                numberOfEnemyTypes = 2;
            }

            if (currentWaveNumber > minWaveNumberForHolzfaeller) {
                numberOfEnemyTypes = 3;
            }

            // determine a random next enemy that is spawned, depending on how many enemies are "unlocked"
            int enemyType = Random.Range(1, numberOfEnemyTypes+1);

            switch (enemyType) {
                case 1:
                    SpawnEnemy(farmerSO);
                    break;
                case 2:
                    SpawnEnemy(dorfschranzeSO);
                    break;
                case 3:
                    SpawnEnemy(holzfaellerSO);
                    break;
                default:
                    SpawnEnemy(farmerSO);
                    break;
            }

            yield return new WaitForSeconds(waveSpawnRate);
        }
    }


    private void SpawnEnemy(EnemyTypeSO enemyTypeSO) {
        Enemy.Create(transform.position, enemyTypeSO);
    }


    public int GetCurrentWaveNumber() {
        return currentWaveNumber;
    }

    public void SetEndlessMode() {
        isEndless = true;
    }

    public bool IsEndless() {
        return isEndless;
    }
}
