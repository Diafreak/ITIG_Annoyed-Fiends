using UnityEngine;


public class SwitchGameMode : MonoBehaviour {

    public GameObject stateDrivenCameras;

    private DoomRay ray;
    private EyeMovement eyeMovement;
    private SwitchCam switchCam;

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
        switchCam = stateDrivenCameras.GetComponent<SwitchCam>();
        switchCam.enabled = true;
    }


    private void Update() {

        if (!switchCam.enabled) {
            return;
        }

        if (Input.GetKeyDown("space")) {
            ray.enabled = !ray.enabled;
            eyeMovement.enabled = !eyeMovement.enabled;
        }
    }


    public void DisableEye() {
        ray.enabled = false;
        eyeMovement.enabled = false;
        switchCam.enabled = false;
    }

    public void EnableEye() {
        ray.enabled = true;
        eyeMovement.enabled = true;
        EnableSwitchCam();
    }


    public bool IsEyeOfDoomActive() {
        return eyeMovement.enabled;
    }


    public void EnableSwitchCam() {
        switchCam.enabled = true;
    }
}