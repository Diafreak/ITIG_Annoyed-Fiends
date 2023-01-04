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
    public GameObject pathTilesLayout;
    public GameObject placeableTilesLayout;
    public GameObject unusableTilesLayout;

    // Default Grid-values
    public int gridWidth  = 10;
    public int gridHeight = 10;
    private float cellSize = 10f;


    private int[,] pathTilesGridCoordinatesArray;
    private int[,] placeableTilesGridCoordinatesArray;
    private int[,] unusableTilesGridCoordinatesArray;

    // Parents that have all the specific tiles as their children
    private GameObject pathTiles;
    private GameObject placeableTiles;
    private GameObject unusableTiles;


    private void Awake() {
        pathTiles      = new GameObject("Path Tiles");
        placeableTiles = new GameObject("Placeable Tiles");
        unusableTiles  = new GameObject("Unusable Tiles");

        // Instantiate the Grid
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
            // Constructor for each GridObject
            (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));

        // Set Default for the currently selected Tower-Type
        currentlySelectedTowerTypeSO = null;

        InitializeTileArrays();

        // Instatiate the Map
        //Transform map1Transform = Instantiate(map1SO.prefab, new Vector3(0, 0, 0), Quaternion.identity);
        //map1Transform.transform.localScale = new Vector3(map1SO.scaleX, map1SO.scaleY, map1SO.scaleZ);
        //map1Transform.transform.position = new Vector3(map1SO.positionX, map1SO.positionY, map1SO.positionZ);

        //map1FromMap.SetActive(false);
    }



    private void Update() {
        // Build Tower
        if (Input.GetMouseButtonDown(0) && MouseIsNotOverUI() && currentlySelectedTowerTypeSO != null) {
            // Get coordinates of the clicked tile via mouse coordinates
            var gridCoordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            // Get the object on that clicked tile
            GridObject gridObject = grid.GetGridObject(gridCoordinates.x, gridCoordinates.z);

            // Check if clicked tile is already occupied
            if ( TileCanBeBuildOn(gridObject) && TowerIsOnValidTile(gridCoordinates.x, gridCoordinates.z) ) {
                // If tile is free, then build on it
                Vector3 placedTowerWorldPosition = grid.GetWorldPosition(gridCoordinates.x, gridCoordinates.z);
                // Create the Tower-Visual
                PlacedTower placedTower = PlacedTower.Create(placedTowerWorldPosition, currentlySelectedTowerTypeSO);
                // Write created Tower in the Grid-Array
                gridObject.SetPlacedTower(placedTower);

                currentlySelectedTowerTypeSO = null;
            } else {
                GridUtils.CreateWorldTextPopup("Cannot Build Here!", GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));
            }

            pathTiles.SetActive(false);
            placeableTiles.SetActive(false);
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

        foreach(TowerTypeSO towerTypeSO in towerTypeSOList) {
            if (towerTypeSO.name == selectedTowerName) {
                currentlySelectedTowerTypeSO = towerTypeSO;

                if (currentlySelectedTowerTypeSO.name == "Gargoyle") {
                    pathTiles.SetActive(true);
                    placeableTiles.SetActive(false);
                } else {
                    pathTiles.SetActive(false);
                    placeableTiles.SetActive(true);
                }
                break;
            };
        }
    }


    // Convert the world coordinates of the LayoutTiles in Grid-Coordinates and save them in separate arrays
    // so they only have to be calculated once at the start and are then available during runtime
    private void InitializeTileArrays() {

        // Path Tiles
        pathTilesLayout.SetActive(true);
        GameObject[] pathTilesFromLayoutArray = GameObject.FindGameObjectsWithTag("Path");
        pathTilesLayout.SetActive(false);

        if (pathTilesFromLayoutArray != null) {
            pathTilesGridCoordinatesArray = new int[pathTilesFromLayoutArray.Length, 2];

            int index = 0;
            foreach (GameObject pathTile in pathTilesFromLayoutArray) {
                var coordinates = grid.GetXZ(pathTile.transform.position);
                pathTilesGridCoordinatesArray[index, 0] = coordinates.x;
                pathTilesGridCoordinatesArray[index, 1] = coordinates.z;

                GridTile.Create(grid.GetWorldPosition(coordinates.x, coordinates.z), gridTileSO, pathTiles.transform);
                index++;
            }
            pathTiles.SetActive(false);
        }

        // Placeable Tiles
        placeableTilesLayout.SetActive(true);
        GameObject[] placeableTilesFromLayoutArray = GameObject.FindGameObjectsWithTag("Placeable");
        placeableTilesLayout.SetActive(false);

        if (placeableTilesFromLayoutArray != null) {
            placeableTilesGridCoordinatesArray = new int[placeableTilesFromLayoutArray.Length, 2];

            int index = 0;
            foreach (GameObject placeableTile in placeableTilesFromLayoutArray) {
                var coordinates = grid.GetXZ(placeableTile.transform.position);
                placeableTilesGridCoordinatesArray[index, 0] = coordinates.x;
                placeableTilesGridCoordinatesArray[index, 1] = coordinates.z;

                GridTile.Create(grid.GetWorldPosition(coordinates.x, coordinates.z), gridTileSO, placeableTiles.transform);
                index++;
            }
            placeableTiles.SetActive(false);
        }

        // Unusable Tiles
        unusableTilesLayout.SetActive(true);
        GameObject[] unusableTilesFromLayoutArray = GameObject.FindGameObjectsWithTag("Unusable");
        unusableTilesLayout.SetActive(false);

        if (unusableTilesFromLayoutArray != null) {
            unusableTilesGridCoordinatesArray = new int[unusableTilesFromLayoutArray.Length, 2];

            int index = 0;
            foreach (GameObject unusableTile in unusableTilesFromLayoutArray) {
                var coordinates = grid.GetXZ(unusableTile.transform.position);
                unusableTilesGridCoordinatesArray[index, 0] = coordinates.x;
                unusableTilesGridCoordinatesArray[index, 1] = coordinates.z;

                GridTile.Create(grid.GetWorldPosition(coordinates.x, coordinates.z), gridTileSO, unusableTiles.transform);
                index++;
            }
            unusableTiles.SetActive(false);
        }
    }


    private bool TowerIsOnValidTile(int x, int z) {
        return     ( currentlySelectedTowerTypeSO.name == "Gargoyle" && IsPath(x, z) )
                || ( currentlySelectedTowerTypeSO.name != "Gargoyle" && IsPlacable(x, z) );
    }


    private bool IsPath(int x, int z) {
        for (int i = 0; i < pathTilesGridCoordinatesArray.Length/2; i++) {
            if (pathTilesGridCoordinatesArray[i, 0] == x && pathTilesGridCoordinatesArray[i, 1] == z) {
                return true;
            }
        }
        return false;
    }

    private bool IsPlacable(int x, int z) {
        for (int i = 0; i < placeableTilesGridCoordinatesArray.Length/2; i++) {
            if (placeableTilesGridCoordinatesArray[i, 0] == x && placeableTilesGridCoordinatesArray[i, 1] == z) {
                return true;
            }
        }
        return false;
    }

    private bool IsUnusable(int x, int z) {
        for (int i = 0; i < unusableTilesGridCoordinatesArray.Length/2; i++) {
            if (unusableTilesGridCoordinatesArray[i, 0] == x && unusableTilesGridCoordinatesArray[i, 1] == z) {
                return true;
            }
        }
        return false;
    }


    private bool TileCanBeBuildOn(GridObject gridObject) {
        return gridObject != null && gridObject.CanBuild();
    }


    // Check if the Mouse if over the UI to prevent clicking through it
    private bool MouseIsNotOverUI() {
        return !EventSystem.current.IsPointerOverGameObject();
    }

}
