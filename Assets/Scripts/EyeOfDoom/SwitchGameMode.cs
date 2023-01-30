using UnityEngine;


public class SwitchGameMode : MonoBehaviour {

    [SerializeField] private GameObject crosshairs;

    private EyeMoovement gamemode;
    private DoomRay ray;


    void Start() {
        gamemode = gameObject.GetComponent<EyeMoovement>();
        ray = gameObject.GetComponent<DoomRay>();
    }


    void Update() {
        if(Input.GetKeyDown("space")){
            gamemode.enabled = !gamemode.enabled;
            ray.enabled = !ray.enabled;
            crosshairs.SetActive(true);
        }
    }
}
