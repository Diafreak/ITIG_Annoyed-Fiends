using UnityEngine;


public class EyeMovement : MonoBehaviour {

    [Header("Sensitivity")]
    public float sensitivityX;
    public float sensitivityY;

    [Header("Eye Camera")]
    public Transform eyeCam;
    public Transform eyeBall;

    [Header("UI")]
    public GameObject crosshairUI;

    [Header("Mouse Smoothing (Higher Value = less smoothing)")]
    public float smoothing = 10f;
    private float xAccumulator;
    private float yAccumulator;

    private float rotationX;
    private float rotationY;


    private void OnEnable() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshairUI.SetActive(true);
    }

    private void OnDisable() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairUI.SetActive(false);
    }


    private void Update() {
        //reads mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * (Time.deltaTime/Time.timeScale) * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * (Time.deltaTime/Time.timeScale) * sensitivityY;

        // Mouse smoothing
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, smoothing * (Time.deltaTime/Time.timeScale));
        yAccumulator = Mathf.Lerp(yAccumulator, mouseY, smoothing * (Time.deltaTime/Time.timeScale));

        rotationY += xAccumulator;
        rotationX -= yAccumulator;

        //prevent player to look more then 90° up or down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotates camera
        eyeCam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        //rotates eye to follow Camera
        eyeBall.localRotation = Quaternion.Euler(90, rotationY, 0);
    }
}