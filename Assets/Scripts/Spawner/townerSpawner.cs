using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townerSpawner : MonoBehaviour
{
    //enemytypes to spawn
    public Transform bauer;
    public Transform dorfschranze;
    public Transform boss;

    public float spawnRate = 0.5f;
    public float timeBetweenWaves = 5f;
    public float waveSpawnRate = 0.5f;
    float countdown = 2f;
    int waveNumber = 0;

    // Update is called once per frame
    void Update()
    {

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -=Time.deltaTime;
    }

    IEnumerator SpawnWave ()
    {
        Debug.Log("Wave Spawned");
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waveSpawnRate);
        }

        
    }

    void SpawnEnemy()
    {
        Instantiate(bauer, transform.position, transform.rotation);
    }
}
