using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour
{
    // List with all available towers
    [SerializeField] private List<TowerTypeSO> towerTypeSOList;
    // current selected Tower-Type
    private TowerTypeSO towerTypeSO;

    // Collider Mask to check where the mouse has clicked on the grid
    [SerializeField] private LayerMask mouseColliderLayerMask;
    // Grid that holds all objects on it
    private GridXZ<GridObject> grid;

    // Default Grid-values
    public int gridWidth  = 10;
    public int gridHeight = 10;
    public float cellSize = 10f;

    private void Awake() {
        // Instantiate the Grid
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
            // Constructor for each GridObject
            (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));

        // Set Default for the currently selected Tower-Type
        towerTypeSO = towerTypeSOList[0];
    }


    // Object-definition of the object that is placed on the Grid
    public class GridObject {

        // Reference to the grid | contains a GridObject on each grid-position
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private PlacedTower placedTower;

        // constructor
        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }


        public void SetPlacedTower(PlacedTower placedTower) {
            this.placedTower = placedTower;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearPlacedTower() {
            this.placedTower = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public PlacedTower GetPlacedTower() {
            return this.placedTower;
        }

        public bool CanBuild() {
            return placedTower == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + placedTower;
        }
    }


    private void Update() {
        // Build Tower
        if (Input.GetMouseButtonDown(0) && !MouseIsOverUI()) {
            // Get coordinates of the clicked tile via mouse coordinates
            var coordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            // Get the object on that clicked tile
            GridObject gridObject = grid.GetGridObject(coordinates.x, coordinates.z);

            // Check if clicked tile is already occupied
            if (gridObject != null && gridObject.CanBuild()) {
                // If tile is free, then build on it
                Vector3 placedTowerWorldPosition = grid.GetWorldPosition(coordinates.x, coordinates.z);
                // Create the Tower-Visual
                PlacedTower placedTower = PlacedTower.Create(placedTowerWorldPosition, new Vector2(coordinates.x, coordinates.z), towerTypeSO);
                // Write created Tower in the Grid-Array
                gridObject.SetPlacedTower(placedTower);
            } else {
                GridUtils.CreateWorldTextPopup("Cannot Build Here!", GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            }
        }

        // Destroy Tower
        if (Input.GetMouseButtonDown(1)) {
            // Get clicked Grid-Tile
            GridObject gridObject = grid.GetGridObject(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            // Get the Tower on that Tile
            PlacedTower placedTower = gridObject.GetPlacedTower();
            if (placedTower != null ) {
                placedTower.DestroySelf();
                gridObject.ClearPlacedTower();
            }
        }

        // Cycle through building variants
        //if (Input.GetKeyDown(KeyCode.Alpha1)) { towerTypeSO = towerTypeSOList[0]; }
        //if (Input.GetKeyDown(KeyCode.Alpha2)) { towerTypeSO = towerTypeSOList[1]; }
        //if (Input.GetKeyDown(KeyCode.Alpha3)) { towerTypeSO = towerTypeSOList[2]; }
    }

    // Gets called by the UI-Buttons and sets the current placable towerType
    public void SetSelectedTower(String selectedTowerName) {
        foreach(TowerTypeSO towerType in towerTypeSOList) {
            if (towerType.name == selectedTowerName) {
                towerTypeSO = towerType;
                break;
            };
        }
    }

    // Check if the Mouse if over the UI to prevent clicking through it
    private bool MouseIsOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
