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
    private TowerTypeSO currentlySelectedTowerTypeSO;

    // Collider Mask to check where the mouse has clicked on the grid
    [SerializeField] private LayerMask mouseColliderLayerMask;
    // Grid that holds all objects on it
    private GridXZ<GridObject> grid;

    public GridTileSO gridTileSO;

    public MapSO map1SO;
    public GameObject map1FromMap;
    //public Transform enemyPrefab;
    public GameObject path;

    // Default Grid-values
    public int gridWidth  = 10;
    public int gridHeight = 10;
    private float cellSize = 10f;


    int[,] pathGridCoordinatesArray;


    private void Awake() {
        // Instantiate the Grid
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
            // Constructor for each GridObject
            (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));

        // Set Default for the currently selected Tower-Type
        currentlySelectedTowerTypeSO = towerTypeSOList[0];


        // Grid
        // 0,2 1,2 2,2
        // 0,1 1,1 2,1
        // 0,0 1,0 2,0
        //Array: [Zeile, Spalte]
        path.SetActive(true);
        GameObject[] pathGameObjectArray = GameObject.FindGameObjectsWithTag("Path");
        path.SetActive(false);
        if (pathGameObjectArray != null) {
            pathGridCoordinatesArray = new int[pathGameObjectArray.Length, 2];
            int index = 0;

            foreach (GameObject pathObject in pathGameObjectArray) {
                var coordinates = grid.GetXZ(pathObject.transform.position);
                pathGridCoordinatesArray[index, 0] = coordinates.x;
                pathGridCoordinatesArray[index, 1] = coordinates.z;
                index++;
            }
        }

        // initialize a visual Grid-Tile for the hover-effect on every grid-tile
        for (int x = 0; x < gridWidth; x++) {
            for (int z = 0; z < gridHeight; z++) {
                if (!IsPath(x, z)) {
                    Vector3 worldPosition = grid.GetWorldPosition(x, z);
                    GridTile.Create(worldPosition, gridTileSO);
                }
            }
        }

        // Instatiate the Map
        Transform map1Transform = Instantiate(map1SO.prefab, new Vector3(0, 0, 0), Quaternion.identity);
        map1Transform.transform.localScale = new Vector3(map1SO.scaleX, map1SO.scaleY, map1SO.scaleZ);
        map1Transform.transform.position = new Vector3(map1SO.positionX, map1SO.positionY, map1SO.positionZ);

        map1FromMap.SetActive(false);

        //Transform enemyTransform = Instantiate(enemyPrefab, new Vector3(142, -5, 30), Quaternion.identity);
        //enemyTransform.transform.localScale = new Vector3(3, 3, 3);
    }



    private void Update() {
        // Build Tower
        if (Input.GetMouseButtonDown(0) && !MouseIsOverUI()) {
            // Get coordinates of the clicked tile via mouse coordinates
            var gridCoordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            // Get the object on that clicked tile
            GridObject gridObject = grid.GetGridObject(gridCoordinates.x, gridCoordinates.z);

            // Check if clicked tile is already occupied
            if (gridObject != null && gridObject.CanBuild() && !IsPath(gridCoordinates.x, gridCoordinates.z)) {
                // If tile is free, then build on it
                Vector3 placedTowerWorldPosition = grid.GetWorldPosition(gridCoordinates.x, gridCoordinates.z);
                // Create the Tower-Visual
                PlacedTower placedTower = PlacedTower.Create(placedTowerWorldPosition, currentlySelectedTowerTypeSO);
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

            if (gridObject != null) {
                // Get the Tower on that Tile
                PlacedTower placedTower = gridObject.GetPlacedTower();

                if (placedTower != null ) {
                    // Destroy tower-visual
                    placedTower.DestroySelf();
                    // Clear Tower-Data from the Grid-Array
                    gridObject.ClearPlacedTower();
                }
            }
        }
    }

    // Gets called by the UI-Buttons and sets the current placable towerType
    public void SetSelectedTower(String selectedTowerName) {
        foreach(TowerTypeSO towerType in towerTypeSOList) {
            if (towerType.name == selectedTowerName) {
                currentlySelectedTowerTypeSO = towerType;
                break;
            };
        }
    }

    private bool IsPath(int x, int z) {
        for (int i = 0; i < pathGridCoordinatesArray.Length/2; i++) {
            if (pathGridCoordinatesArray[i, 0] == x && pathGridCoordinatesArray[i, 1] == z) {
                return true;
            }
        }

        return false;
    }


    // Check if the Mouse if over the UI to prevent clicking through it
    private bool MouseIsOverUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
