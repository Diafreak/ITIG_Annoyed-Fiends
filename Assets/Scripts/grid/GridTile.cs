using UnityEngine;


public class GridTile : MonoBehaviour {

    public enum TileType {
        placeable,
        path
    }

    private TileType type;

    private Color color;
    private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Renderer tileRenderer;

    private GridTileSO gridTileSO;

    private GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
        tileRenderer = gameObject.GetComponent<Renderer>();
    }


    public static GridTile Create(Vector3 worldPosition, GridTileSO givenGridTileSO, Transform parent, TileType _type) {

        Transform gridTileTransform =
            Instantiate(
                // Visual
                givenGridTileSO.visual,
                // Position
                worldPosition,
                // Rotation
                Quaternion.identity,
                // Parent
                parent
            );

        GridTile gridTile = gridTileTransform.GetChild(0).GetComponent<GridTile>();
        gridTile.gridTileSO = givenGridTileSO;
        gridTile.color = givenGridTileSO.defaultColor;
        gridTile.highlight = gridTileTransform.GetChild(0).GetChild(0).gameObject;
        gridTile.type = _type;

        return gridTile;
    }


    private void Update() {
        CheckIfPlayerHasEnoughMoney();
    }

    private void OnMouseEnter() {
        highlight.SetActive(true);
    }

    private void OnMouseExit() {
        highlight.SetActive(false);
    }

    private void OnDisable() {
        // Tiles get disabled when placing a tower while the mouse is still over one,
        // so the highlight would stay active on next activation
        highlight.SetActive(false);
    }


    private void CheckIfPlayerHasEnoughMoney() {

        color = gridTileSO.defaultColor;

        if (!gridBuildingSystem.PlayerHasEnoughMoney()) {
            color = gridTileSO.insufficientMoneyColor;
        }

        tileRenderer.material.SetColor("_Color", color);
    }

    public TileType GetTileType() {
        return type;
    }
}
