using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = new Grid(4, 2, 10f, new Vector3(20, 0));
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.SetValue(GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.GetValue(GetMouseWorldPosition()));
        }
    }


    // Z = 0
    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPostition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPostition);
        return worldPosition;
    }
}
