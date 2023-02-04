using UnityEngine;
using UnityEngine.EventSystems;


public class GridBuildingSystem : MonoBehaviour {

    // current selected Tower-Type
    private TowerTypeSO currentlySelectedTowerTypeSO;

    // Collider Mask to check where the Mouse has clicked on the Grid
    [SerializeField] private LayerMask mouseColliderLayerMask;

    // Grid that holds all Objects on it
    private GridXZ<GridObject> grid;

    [Header("Grid Values")]
    public int gridWidth;
    public int gridHeight;
    private float cellSize = 10f;

    [Header("Grid Tiles")]
    // Tower-placement visuals & hovering-effect
    public GridTileSO gridTileSO;
    // References to the tile-layout of the map
    public GameObject placeableTilesLayout;
    public GameObject pathTilesLayout;
    public float pathTileYOffset = 0f;

    // Array that holds all Tile-types at the corresponding grid-coordinates to check tower placement
    private GridTile[,] gridTiles;

    // Parents that have all their specific tiles as their children
    // used to toggle the visual representation of placeable tiles for a tower
    private GameObject pathTiles;
    private GameObject placeableTiles;

    // UI for Upgrading/Selling a Tower
    [Header("Tower UI")]
    public TowerUI towerUI;
    public Transform towerRange;

    // Singleton
    public static GridBuildingSystem instance;



    private void Awake() {

        // Singleton
        if (instance == null) {
            instance = this;
        }

        pathTiles      = new GameObject("Path Tiles");
        placeableTiles = new GameObject("Placeable Tiles");

        gridTiles = new GridTile[gridWidth, gridHeight];

        // Instantiate the Grid
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
            // Constructor for each GridObject
            (GridXZ<GridObject> gridObject, int x, int z) => new GridObject(gridObject, x, z));

        // set Default for the currently selected Tower-Type
        currentlySelectedTowerTypeSO = null;

        InitializeGridTiles();

        towerUI.Hide();
        towerRange.gameObject.SetActive(false);
    }


    private void Update() {

        // Build Tower on Left-Click
        if (Input.GetMouseButtonDown(0) && MouseIsNotOverUI()) {

            // convert Mouse-Coordinates into Grid-Coordinates
            var gridCoordinates = grid.GetXZ(GetClickedTile());

            // get Object on clicked Tile
            GridObject gridObject = grid.GetGridObject(gridCoordinates.x, gridCoordinates.z);

            // check if clicked Tile is occupied
            if (TileCanBeBuildOn(gridObject) && PlayerHasEnoughMoney()) {
                // if tile is free, build on it
                BuildTower(gridObject);

            } else if (TileHasTower(gridObject) && IsPlaceable(gridCoordinates.x, gridCoordinates.z)) {
                // if Tile already has a Tower -> show Upgrade/Sell-Menu + Range
                towerUI.SetTarget(gridObject);
            }

            currentlySelectedTowerTypeSO = null;
            pathTiles.SetActive(false);
            placeableTiles.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1)) {
            PlayerStats.AddMoney(100);
        }
    }



    // ------------------------------
    // Building
    // ------------------------------

    // gets called by the UI-Buttons and sets the current placable Tower-Type
    public void SetSelectedTower(TowerTypeSO towerTypeSO) {
        // hide Upgrade/Sell-Menu + Range
        towerUI.Hide();

        currentlySelectedTowerTypeSO = towerTypeSO;

        // toggle visual Tiles where a Tower can be placed
        if (currentlySelectedTowerTypeSO.name == "Gargoyle") {
            pathTiles.SetActive(true);
            placeableTiles.SetActive(false);
        } else {
            pathTiles.SetActive(false);
            placeableTiles.SetActive(true);
        }
    }


    public PlacedTower GetSelectedTower() {
        return grid.GetGridObject(GetClickedTile()).GetTower();
    }


    private void BuildTower(GridObject gridObject) {
        // get World-Coordinates to build on from Grid-Coordinates
        Vector3 placedTowerWorldPosition = grid.GetWorldPosition(gridObject.GetGridPosition().x, gridObject.GetGridPosition().z);
        // create Tower-Visual
        PlacedTower placedTower = PlacedTower.Create(placedTowerWorldPosition, currentlySelectedTowerTypeSO);
        // write created Tower in the Grid-Array
        gridObject.SetPlacedTower(placedTower);

        // subtract Tower-costs from Player-Money
        PlayerStats.SubtractMoney(currentlySelectedTowerTypeSO.price);

        // Hide placement-tile on the Tower-position
        gridTiles[gridObject.GetGridPosition().x, gridObject.GetGridPosition().z].gameObject.SetActive(false);
    }


    public bool PlayerHasEnoughMoney() {
        return PlayerStats.GetMoney() >= currentlySelectedTowerTypeSO.price;
    }


    // Offset to get the middle of a Grid-Cell because the GridCoordinates-Origin is in the lower left corner
    public Vector3 GetBuildOffset() {
        return new Vector3(cellSize/2, 0, cellSize/2);
    }



    // ------------------------------
    // Tiles
    // ------------------------------

    // Convert the world coordinates of the LayoutTiles in Grid-Coordinates and save them in separate arrays
    // so they only have to be calculated once at the start and are then available during runtime
    private void InitializeGridTiles() {

        // load Tile-Layout from Map into array
        placeableTilesLayout.SetActive(true);
        GameObject[] placeableTilesFromLayout = GameObject.FindGameObjectsWithTag("Placeable");
        placeableTilesLayout.SetActive(false);

        pathTilesLayout.SetActive(true);
        GameObject[] pathTilesFromLayout = GameObject.FindGameObjectsWithTag("Path");
        pathTilesLayout.SetActive(false);

        InitializeGridTileArray(placeableTilesFromLayout, GridTile.TileType.Placeable, placeableTiles);
        InitializeGridTileArray(pathTilesFromLayout,      GridTile.TileType.Path,      pathTiles, pathTileYOffset);
    }


    private void InitializeGridTileArray(GameObject[] tileArray, GridTile.TileType type, GameObject tileParent, float yOffset = 0) {
        if (tileArray == null) {
            return;
        }

        foreach (GameObject tile in tileArray) {
            var coordinates = grid.GetXZ(tile.transform.position);
            gridTiles[coordinates.x, coordinates.z] = GridTile.Create(grid.GetWorldPosition(coordinates.x, coordinates.z), gridTileSO, tileParent.transform, type);
        }
        tileParent.transform.position += new Vector3(0, yOffset, 0);
        tileParent.SetActive(false);
    }


    private bool TileCanBeBuildOn(GridObject gridObject) {
        return  currentlySelectedTowerTypeSO != null
                && gridObject != null
                && gridObject.CanBuild()
                && TowerIsOnValidTile(gridObject.GetGridPosition().x, gridObject.GetGridPosition().z);
    }


    private bool TowerIsOnValidTile(int x, int z) {
        return     ( currentlySelectedTowerTypeSO.name == "Gargoyle" && IsPath(x, z) )
                || ( currentlySelectedTowerTypeSO.name != "Gargoyle" && IsPlaceable(x, z) );
    }


    private bool TileHasTower(GridObject gridObject) {
        return gridObject?.GetTower() != null;
    }


    private bool IsPath(int x, int z) {
        return gridTiles[x, z]?.GetTileType() == GridTile.TileType.Path;
    }


    private bool IsPlaceable(int x, int z) {
        return gridTiles[x, z]?.GetTileType() == GridTile.TileType.Placeable;
    }


    public GridTile GetGridTile(int x, int z) {
        return gridTiles[x, z];
    }


    public void ReactivateGridTile(int x, int z) {
        gridTiles[x, z].gameObject.SetActive(true);
    }



    // ------------------------------
    // Helper
    // ------------------------------

    // Check if the Mouse if over the UI to prevent clicking through it
    private bool MouseIsNotOverUI() {
        return !EventSystem.current.IsPointerOverGameObject();
    }


    private Vector3 GetClickedTile() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            //if (raycastHit.transform.gameObject.CompareTag("Path"))
                return raycastHit.point;
        }
        return new Vector3(-1, -1, -1);
    }


    // Mouse-Position in 3D-Space
    private Vector3 GetMouseWorldPosition3d() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
            return raycastHit.point;
        }
        return Vector3.zero;
    }


    public GridXZ<GridObject> GetGridXZ() {
        return grid;
    }


    public float GetCellSize() {
        return cellSize;
    }

}