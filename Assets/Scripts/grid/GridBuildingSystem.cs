using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour {

    // current selected Tower-Type
    private TowerTypeSO currentlySelectedTowerTypeSO;

    // Collider Mask to check where the Mouse has clicked on the Grid
    [SerializeField] private LayerMask mouseColliderLayerMask;

    // Grid that holds all objects on it
    private GridXZ<GridObject> grid;

    // Grid-values
    public int gridWidth;
    public int gridHeight;
    private float cellSize = 10f;


    // for visual hovering-effect
    public GridTileSO gridTileSO;

    // References to the tile-layout of the map
    public GameObject pathTilesLayout;
    public GameObject placeableTilesLayout;
    public GameObject unusableTilesLayout;

    // Arrays that hold the grid-coordinates of each tile type to check tower placement
    private int[,] pathTilesGridCoordinatesArray;
    private int[,] placeableTilesGridCoordinatesArray;
    private int[,] unusableTilesGridCoordinatesArray;

    // Parents that have all the specific tiles as their children
    // used to toggle the visual representation of placeable tiles for a tower
    private GameObject pathTiles;
    private GameObject placeableTiles;
    private GameObject unusableTiles;


    // UI for Upgradeing/Selling a Tower
    public TowerUI towerUI;


    // Singleton
    public static GridBuildingSystem instance;


    private void Awake() {

        // Singleton
        if (instance == null) {
            instance = this;
        }

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
    }


    private void Update() {

        // Build Tower on Left-Click
        if (Input.GetMouseButtonDown(0) && MouseIsNotOverUI()) {

                // convert Mouse-Coordinates from Click into Grid-Coordinates
                var gridCoordinates = grid.GetXZ(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

                // get the Object on that clicked Tile
                GridObject gridObject = grid.GetGridObject(gridCoordinates.x, gridCoordinates.z);

                // check if clicked Tile is already occupied
                if (TileCanBeBuildOn(gridObject) && PlayerHasEnoughMoney()) {
                    // if tile is free, build on it
                    BuildTower(gridObject);

                // if Tile already has a Tower -> show Upgrade/Sell-Menu
                } else if (TileHasTower(gridObject) && IsPlacable(gridCoordinates.x, gridCoordinates.z)) {
                    towerUI.SetTarget(grid.GetWorldPosition(gridCoordinates.x, gridCoordinates.z));
                    // clear Left-Click
                    currentlySelectedTowerTypeSO = null;
                }

            pathTiles.SetActive(false);
            placeableTiles.SetActive(false);
        }


        // Destroy Tower
        if (Input.GetMouseButtonDown(1)) {
            // get clicked Grid-Tile
            GridObject gridObject = grid.GetGridObject(GridUtils.GetMouseWorldPosition3d(mouseColliderLayerMask));

            if (gridObject != null) {
                // get the Tower on that Tile
                PlacedTower placedTower = gridObject.GetPlacedTower();

                if (placedTower != null ) {
                    // destroy Tower-Visual
                    placedTower.DestroySelf();
                    // clear Tower-Data from the Grid-Array
                    gridObject.ClearPlacedTower();
                }
            }
        }
    }


    // gets called by the UI-Buttons and sets the current placable Tower-Type
    public void SetSelectedTower(TowerTypeSO towerTypeSO) {

        currentlySelectedTowerTypeSO = towerTypeSO;
        // hide Upgrade/Sell-Menu
        towerUI.Hide();

        // toggle visual Tiles where a Tower can be placed
        if (currentlySelectedTowerTypeSO.name == "Gargoyle") {
            pathTiles.SetActive(true);
            placeableTiles.SetActive(false);
        } else {
            pathTiles.SetActive(false);
            placeableTiles.SetActive(true);
        }
    }


    private void BuildTower(GridObject gridObject) {
        // get World-Coordinates to build on from Grid-Coordinates
        Vector3 placedTowerWorldPosition = grid.GetWorldPosition(gridObject.GetPosition().x, gridObject.GetPosition().z);
        // create Tower-Visual
        PlacedTower placedTower = PlacedTower.Create(placedTowerWorldPosition, currentlySelectedTowerTypeSO);
        // write created Tower in the Grid-Array
        gridObject.SetPlacedTower(placedTower);

        // remove Tower-costs from Player-Money
        PlayerStats.money -= currentlySelectedTowerTypeSO.price;
        Debug.Log("Money left: " + PlayerStats.money);

        // clear left-click
        currentlySelectedTowerTypeSO = null;
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


    private bool TileCanBeBuildOn(GridObject gridObject) {
        return  currentlySelectedTowerTypeSO != null
                && gridObject != null
                && gridObject.CanBuild()
                && TowerIsOnValidTile(gridObject.GetPosition().x, gridObject.GetPosition().z);
    }

    private bool TowerIsOnValidTile(int x, int z) {
        return     ( currentlySelectedTowerTypeSO.name == "Gargoyle" && IsPath(x, z) )
                || ( currentlySelectedTowerTypeSO.name != "Gargoyle" && IsPlacable(x, z) );
    }

    private bool TileHasTower(GridObject gridObject) {
        return gridObject != null && gridObject.GetPlacedTower() != null;
    }

    private bool PlayerHasEnoughMoney() {
        if (PlayerStats.money < currentlySelectedTowerTypeSO.price) {
            Debug.Log("Not enough money!");
            return false;
        }
        return true;
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


    // Check if the Mouse if over the UI to prevent clicking through it
    private bool MouseIsNotOverUI() {
        return !EventSystem.current.IsPointerOverGameObject();
    }

}
