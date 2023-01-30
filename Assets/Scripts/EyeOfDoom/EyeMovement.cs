using UnityEngine;


public class EyeMovement : MonoBehaviour {

    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;


    private float rotationX;
    private float rotationY;


    private void OnEnable() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update() {
        //reads mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * (Time.deltaTime/Time.timeScale) * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * (Time.deltaTime/Time.timeScale) * sensitivityY;

        rotationY += mouseX;
        rotationX -= mouseY;

        //prevent player to look more then 90° up or down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotates camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}