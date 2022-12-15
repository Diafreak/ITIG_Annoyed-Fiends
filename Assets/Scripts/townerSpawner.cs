using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class townerSpawner : MonoBehaviour
{
    //enemytypes to spawn
    public GameObject bauer;
    public GameObject dorfschranze;
    public GameObject boss;

    public float spawnRate = 0.5f;
    float timePast = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePast += Time.deltaTime;

        if(spawnRate < timePast){
            timePast = 0.0f;
            Instantiate(bauer, transform.position, transform.rotation);
        }
    }
}
