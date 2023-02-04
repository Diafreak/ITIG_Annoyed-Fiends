using UnityEngine;


public class SwitchGameMode : MonoBehaviour {

    private DoomRay ray;
    private EyeMovement eyeMovement;

    private GameManager gameManager;

    // Singleton
    public static SwitchGameMode instance;


    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
    }


    private void Start() {
        gameManager = GameManager.instance;

        ray = GetComponent<DoomRay>();
        ray.enabled = false;
        eyeMovement = GetComponent<EyeMovement>();
        eyeMovement.enabled = false;
    }


    private void Update() {
        if (Input.GetKeyDown("space")) {
            ray.enabled = !ray.enabled;
            eyeMovement.enabled = !eyeMovement.enabled;
            gameManager.crosshairUI.SetActive(!gameManager.crosshairUI.activeSelf);
        }
    }


    public void DisableEye() {
        ray.enabled = false;
        eyeMovement.enabled = false;
    }

    public void EnableEye() {
        ray.enabled = true;
        eyeMovement.enabled = true;
    }


    public bool IsEyeOfDoomActive() {
        return eyeMovement.enabled;
    }
}