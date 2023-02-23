using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    [Header("Enemies")]
    public EnemyTypeSO farmerSO;
    public EnemyTypeSO dorfschranzeSO;
    public EnemyTypeSO holzfaellerSO;

    [Header("Attributes")]
    public float waveSpawnRate = 1f;

    public int minWaveNumberForDorfschranze = 3;
    public int minWaveNumberForTank = 8;

    [Header("Money")]
    public int baseMoneyAfterRound = 100;


    private int currentWaveNumber;
    private int maxWaveNumber;
    private int waveMultiplicator;
    private bool isEndless;

    private int moneyAfterRound;

    private int numberOfEnemiesUnlocked;

    private StartAndSpeedupButton startAndSpeedupButton;
    private GameManager gameManager;
    private TowerShop towerShop;

    // Singleton
    public static EnemySpawner instance;



    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }


    private void Start() {
        gameManager = GameManager.instance;
        startAndSpeedupButton = StartAndSpeedupButton.instance;
        towerShop = TowerShop.instance;

        enemiesAlive = 0;
        currentWaveNumber = 0;
        waveMultiplicator = 10;
        isEndless = false;

        moneyAfterRound = baseMoneyAfterRound;

        numberOfEnemiesUnlocked = 1;

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

        if (startAndSpeedupButton.GetCurrentState() != GameManager.GameState.beforeNewRound) {
            // earn Money after each successful Wave
            moneyAfterRound = baseMoneyAfterRound + currentWaveNumber;
            PlayerStats.AddMoney(moneyAfterRound);
        }

        towerShop.ResetGargoyleTimer();
        startAndSpeedupButton.SetGameStateToBeforeNewRound();
    }


    public IEnumerator SpawnWave () {
        currentWaveNumber++;
        enemiesAlive = currentWaveNumber * waveMultiplicator;
        gameManager.SetCurrentWaveNumber(currentWaveNumber);

        for (int i = 0; i < currentWaveNumber * waveMultiplicator; i++) {

            if (currentWaveNumber >= minWaveNumberForDorfschranze && currentWaveNumber < minWaveNumberForTank) {
                numberOfEnemiesUnlocked = 2;
            }

            if (currentWaveNumber >= minWaveNumberForTank) {
                numberOfEnemiesUnlocked = 3;
            }

            // determine a random next enemy that is spawned, depending on how many enemies are "unlocked"
            int enemyType = Random.Range(1, (numberOfEnemiesUnlocked + 1)*2 - 1);

            switch (enemyType) {
                case 1: case 2: case 6:
                    SpawnEnemy(farmerSO);
                    break;
                case 3: case 4:
                    SpawnEnemy(dorfschranzeSO);
                    break;
                case 5:
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
