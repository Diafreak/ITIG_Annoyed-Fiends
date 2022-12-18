using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] private Color defaultColor, highlightColor;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //private GridTileSO gridTileSO;

    public static void Create(Vector3 worldPosition, GridTileSO givenGridTileSO) {

        Transform gridTileTransform =
            Instantiate(
                // Visual
                givenGridTileSO.visual,
                // Position
                worldPosition,
                // Rotation
                Quaternion.identity
            );

        //GridTile gridTile = gridTileTransform.GetComponent<GridTile>();

        //gridTile.gridTileSO = givenGridTileSO;
        //gridTile.spriteRenderer.color = gridTile.defaultColor;
        //gridTile.highlight.GetComponent<SpriteRenderer>().color = gridTile.highlightColor;
    }


    private void OnMouseEnter() {
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    private void OnMouseExit() {
        spriteRenderer.color = defaultColor;
        highlight.SetActive(false);
    }
}
