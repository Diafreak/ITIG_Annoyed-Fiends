using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    [SerializeField] private Color defaultColor, highlightColor;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public static void Create(Vector3 worldPosition, GridTileSO givenGridTileSO, Transform parent) {

        //Transform gridTileTransform =
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
    }


    private void OnMouseEnter() {
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    private void OnMouseExit() {
        spriteRenderer.color = defaultColor;
        highlight.SetActive(false);
    }

    private void OnDisable() {
        // because they get disabled when placing a tower while the mouse is still
        // over the tile the highlight would stay active until the mouse moves over it again
        highlight.SetActive(false);
    }
}
