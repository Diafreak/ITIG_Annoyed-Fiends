using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    private Color color;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;

    GridBuildingSystem gridBuildingSystem;

    private GridTileSO gridTileSO;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
    }


    public static void Create(Vector3 worldPosition, GridTileSO givenGridTileSO, Transform parent) {

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
    }


    private void OnMouseEnter() {
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().color = gridTileSO.highlightColor;
    }

    private void OnMouseExit() {
        spriteRenderer.color = color;
        highlight.SetActive(false);
    }

    private void OnEnable() {
        if (gridBuildingSystem != null) {
            if (gridBuildingSystem.PlayerHasEnoughMoney()) {
                color = gridTileSO.defaultColor;
            } else {
                color = gridTileSO.insufficientMoneyColor;
            }
        }

        spriteRenderer.color = color;
    }

    private void OnDisable() {
        // because they get disabled when placing a tower while the mouse is still
        // over the tile the highlight would stay active until the mouse moves over it again
        highlight.SetActive(false);
    }
}
