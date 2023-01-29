using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGameMode : MonoBehaviour
{
    [SerializeField] GameObject crosshairs;
    
    EyeMoovement gamemode;
    DoomRay ray;
    
    // Start is called before the first frame update
    void Start()
    {
        gamemode = gameObject.GetComponent<EyeMoovement>();
        ray = gameObject.GetComponent<DoomRay>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            gamemode.enabled = !gamemode.enabled;
            ray.enabled = !ray.enabled;
            crosshairs.SetActive(true);
        }
    }
}
