using UnityEngine;


public class EyeMovement : MonoBehaviour {

    [Header("Sensitivity")]
    [Range(1, 50)] public float sensitivity = 25;

    [Header("Eye Camera")]
    public Transform eyeCam;
    public Transform eyeBall;

    [Header("UI")]
    public GameObject crosshairUI;

    [Header("Mouse Smoothing (Higher Value = less smoothing)")]
    [Range(1, 25)] public float smoothing = 10f;
    private float xAccumulator;
    private float yAccumulator;

    private float rotationX;
    private float rotationY;


    private void OnEnable() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshairUI.SetActive(true);

        smoothing   = PlayerPrefs.GetFloat("MouseSmoothing",   10);
        sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 25);
    }


    private void OnDisable() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairUI.SetActive(false);
    }


    private void Update() {
        // read Mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * (Time.deltaTime/Time.timeScale) * sensitivity*10;
        float mouseY = Input.GetAxisRaw("Mouse Y") * (Time.deltaTime/Time.timeScale) * sensitivity*10;

        // Mouse smoothing
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, smoothing * (Time.deltaTime/Time.timeScale));
        yAccumulator = Mathf.Lerp(yAccumulator, mouseY, smoothing * (Time.deltaTime/Time.timeScale));

        rotationY += xAccumulator;
        rotationX -= yAccumulator;

        // prevent Player to look more then 90° up or down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // rotates Camera around both axis
        eyeCam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        // rotates eye to follow Camera
        eyeBall.localRotation = Quaternion.Euler(90, rotationY, 0);
        
    }
}