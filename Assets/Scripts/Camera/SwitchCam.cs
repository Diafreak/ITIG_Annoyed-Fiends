using UnityEngine;
using UnityEngine.InputSystem;


public class SwitchCam : MonoBehaviour {

    [SerializeField] private InputAction action;

    private Animator animator;

    private bool MainCam = true;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        action.performed += _ => SwitchState();
    }


    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }


    private void SwitchState() {
        if (MainCam) {
            animator.Play("EyeCam");
        } else {
            animator.Play("MainCam");
        }
        MainCam = !MainCam;
    }
}