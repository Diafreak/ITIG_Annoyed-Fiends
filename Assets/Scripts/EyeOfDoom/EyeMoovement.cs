using UnityEngine;
using UnityEngine.InputSystem;


public class EyeMoovement : MonoBehaviour {

    public float sensitivityX;
    public float sensitivityY;
    public bool mouselock = false;

    public Transform orientation;

    private float rotationX;
    private float rotationY;


    private void Start() {    
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    private void Update() {

        if(Input.GetKeyDown("space")){
            mouselock = !mouselock;
        }

        if (mouselock = false){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //reads mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        rotationY += mouseX;
        rotationX -= mouseY;

        //prevent player to look more then 90° up or down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotates camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
