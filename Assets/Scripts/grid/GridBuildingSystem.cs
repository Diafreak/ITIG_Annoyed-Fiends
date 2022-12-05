using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private List<PlacedTowerTypeSO> placedTowerTypeSOList;
    private PlacedTowerTypeSO placedTowerTypeSO;

    [SerializeField] private LayerMask mouseColliderLayerMask;
    private GridXZ<GridObject> grid;

    public int gridWidth  = 10;
    public int gridHeight = 10;
    public float cellSize = 10f;

    private void Awake() {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));

        placedTowerTypeSO = placedTowerTypeSOList[0];
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

        public Transform GetPlacedTower() {
            return this.transform;
        }

        public bool CanBuild() {
            return transform == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + transform;
        }
/*
        public void DestroySelf() {
            Destroy(transform);
        }
*/
    }


    private void Update() {

        // Build Tower
        if (Input.GetMouseButtonDown(0)) {
            // Get coordinates of the clicked tile via mouse coordinates
            var coordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            // Get the object on that clicked tile
            GridObject gridObject = grid.GetGridObject(coordinates.x, coordinates.z);

            // Check if clicked tile is already occupied
            if (gridObject != null && gridObject.CanBuild()) {
                // If tile is free, then build on it
                Transform buildTransform = Instantiate(
                    // Visual
                    placedTowerTypeSO.prefab,
                    // Position
                    grid.GetWorldPosition(coordinates.x, coordinates.z),
                    // Rotation
                    Quaternion.Euler(0, 1, 0));
                gridObject.SetTransform(buildTransform);
            } else {
                GridUtils.CreateWorldTextPopup("Cannot Build Here!", GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            }
        }

/*
        // Destroy tower
        if (Input.GetMouseButtonDown(1)) {
            GridObject gridObject = grid.GetGridObject(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            Transform placedTower = gridObject.GetPlacedTower();
            if (placedTower != null ) {
                placedTower.D
            }
        }
*/

        // Cycle through building variants
        if (Input.GetKeyDown(KeyCode.Alpha1)) { placedTowerTypeSO = placedTowerTypeSOList[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { placedTowerTypeSO = placedTowerTypeSOList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { placedTowerTypeSO = placedTowerTypeSOList[2]; }
    }

}
