using UnityEngine;


public class GridTile : MonoBehaviour {

    public enum TileType {
        Placeable,
        Path
    }

    private TileType tileType;

    private GameObject highlight;

    private Renderer tileRenderer;

    private GridTileSO gridTileSO;
    private GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
        tileRenderer = gameObject.GetComponent<Renderer>();
    }


    public static GridTile Create(Vector3 worldPosition, GridTileSO givenGridTileSO, Transform parent, TileType type) {

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
        gridTile.highlight = gridTileTransform.GetChild(0).GetChild(0).gameObject;
        gridTile.tileType = type;
        gridTile.gameObject.tag = type.ToString();

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
        if (gridBuildingSystem.PlayerHasEnoughMoney()) {
            tileRenderer.material.SetColor("_Color", gridTileSO.defaultColor);
        } else {
            tileRenderer.material.SetColor("_Color", gridTileSO.insufficientMoneyColor);
        }
    }


    public TileType GetTileType() {
        return tileType;
    }
}
