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

        ray         = gameObject.GetComponent<DoomRay>();
        ray.enabled = false;
        eyeMovement         = gameObject.GetComponent<EyeMovement>();
        eyeMovement.enabled = false;
    }


    private void Update() {
        if (Input.GetKeyDown("space")) {
            ray.enabled = !ray.enabled;
            eyeMovement.enabled = !eyeMovement.enabled;
            gameManager.crosshairUI.SetActive(!gameManager.crosshairUI.activeSelf);
        }
    }


    public void DisableRay() {
        ray.enabled = false;
    }

    public void EnableRay() {
        ray.enabled = true;
    }


    public bool IsEyeOfDoomActive() {
        return eyeMovement.enabled;
    }
}