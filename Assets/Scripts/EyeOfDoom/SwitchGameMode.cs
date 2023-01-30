using UnityEngine;


public class SwitchGameMode : MonoBehaviour {

    [SerializeField] private GameObject crosshairs;

    private DoomRay ray;


    private void Start() {
        ray = gameObject.GetComponent<DoomRay>();
    }


    private void Update() {
        if(Input.GetKeyDown("space")){
            ray.enabled = !ray.enabled;
            crosshairs.SetActive(true);
        }
    }
}