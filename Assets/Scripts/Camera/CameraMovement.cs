using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += new Vector3(0, 0, 0.5f);
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.position += new Vector3(0, 0, -0.5f);
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.position += new Vector3(-0.5f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position += new Vector3(0.5f, 0, 0);
        }
    }
}
