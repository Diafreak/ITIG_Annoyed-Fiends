using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    private GridXZ<GridObject> grid;


    private void Awake() {
        int gridWidth  = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));
    }


    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        // constructor
        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
            this.transform = null;
        }


        public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearTransform() {
            this.transform = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild() {
            return transform == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + transform;
        }
    }


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var coordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            GridObject gridObject = grid.GetGridObject(coordinates.x, coordinates.z);

            if (gridObject != null && gridObject.CanBuild()) {
                Transform buildTransform = Instantiate(testTransform, grid.GetWorldPosition(coordinates.x, coordinates.z), Quaternion.identity);
                gridObject.SetTransform(buildTransform);
            } else {
                GridUtils.CreateWorldTextPopup("Cannot Build Here!", GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            }
        }
    }
}
