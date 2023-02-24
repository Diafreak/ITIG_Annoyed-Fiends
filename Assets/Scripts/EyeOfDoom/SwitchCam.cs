using UnityEngine;
using UnityEngine.InputSystem;


public class SwitchCam : MonoBehaviour {

    [SerializeField] private InputAction action;

    private Animator animator;

    // start with Main Camera because the priority is higher
    private bool mainCameraIsActive = true;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        // activates SwitchtState without passing parameters
        action.performed += _ => SwitchState();
    }


    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }


    // switch Cameras
    private void SwitchState() {

        if (mainCameraIsActive) {
            animator.Play("EyeCam");
            mainCameraIsActive = false;
        } else {
            animator.Play("MainCam");
            mainCameraIsActive = true;
        }
    }
}