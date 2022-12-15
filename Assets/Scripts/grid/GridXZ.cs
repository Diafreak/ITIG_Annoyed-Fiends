using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Generic Grid-Class that generates a grid on the X & Z-axis
public class GridXZ<TGridObject> {

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs: EventArgs {
        public int x;
        public int z;
    } 

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    // Constructor
    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject) {
        this.width          = width;
        this.height         = height;
        this.cellSize       = cellSize;
        this.originPosition = originPosition;

        // Array that stores the objects placed on the grid
        gridArray = new TGridObject[width, height];

        // initialize the grid-array to prevent null-error
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int z = 0; z < gridArray.GetLength(1); z++) {
                gridArray[x, z] = createGridObject(this, x, z);
            }
        }

        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int z = 0; z < gridArray.GetLength(1); z++) {
                    debugTextArray[x, z] = GridUtils.CreateWorldText(gridArray[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, cellSize) * 0.5f, 40, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0),  GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
            };
        }
    }


    // manualy trigger an update on a specific grid-position when something on it is updated
    public void TriggerGridObjectChanged(int x, int z) {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs {x = x, z = z});
    }


    // sets a grid-object on a given world-position and snaps it to the grid
    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        var coordinates = GetXZ(worldPosition);
        SetGridObject(coordinates.x, coordinates.z, value);
    }

    // sets a grid-object on a given grid-position on the grid
    public void SetGridObject(int x, int z, TGridObject value) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            gridArray[x, z] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs {x = x, z = z});
        }
    }


    // returns the grid-object to a given world-position
    public TGridObject GetGridObject(Vector3 worldPosition) {
        var coordinates = GetXZ(worldPosition);
        return GetGridObject(coordinates.x, coordinates.z);
    }

    // returns the grid-object to a given grid-position
    public TGridObject GetGridObject(int x, int z) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            return gridArray[x, z];
        }
        return default(TGridObject); // for objects -> "null" | for int -> "-1" | for bool -> "false"
    }


    // takes a grid-position and returns a world-position
    public Vector3 GetWorldPosition(int x, int z) {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    // takes a world-position and returns a grid-position
    public (int x, int z) GetXZ(Vector3 worldPosition) {
        int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        int z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        return (x, z);
    }

}
