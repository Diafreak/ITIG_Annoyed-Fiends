using System.Collections;
using UnityEngine;
using TMPro;


public class TownerSpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    [Header("Enemy Types")]
    public Transform towner;
    public Transform dorfschranze;
    public Transform boss;

    [Header("Attributes")]
    public float timeBetweenWaves = 5f;
    public float waveSpawnRate = 1f;
    float countdown = 2f;
    public int waveNumber = 0;

    [Header("UI Text Fields")]
    public TMP_Text waveNumberText;
    public TMP_Text nextWaveCountdownText;


    private void Start() {
        waveNumberText.text = "Wave " + waveNumber;
    }


    private void Update() {

        if (enemiesAlive > 0) {
            return;
        }

        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -=Time.deltaTime;
        // make sure the countdown doesn't go into negative values
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        nextWaveCountdownText.text = "Next Wave in: " + string.Format("{0:00.0}", countdown).Replace(",", ".");
    }


    IEnumerator SpawnWave () {
        Debug.Log("Wave Spawned");
        waveNumber++;
        enemiesAlive = waveNumber+5;

        waveNumberText.text = "Wave " + waveNumber;

        for (int i = 0; i < waveNumber+5; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(waveSpawnRate);
        }
    }

    private void SpawnEnemy() {
        Instantiate(towner, transform.position, transform.rotation);
    }
}
