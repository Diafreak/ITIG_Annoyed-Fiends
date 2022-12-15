using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedTower : MonoBehaviour
{
    private PlacedTowerTypeSO placedTowerTypeSO;
    private Vector2 origin;

    public static PlacedTower Create(Vector3 worldPosition, Vector2 origin, PlacedTowerTypeSO placedTowerTypeSO) {
        // create a Tower on the clicked position
        Transform placedTowerTransform =
            Instantiate(
                // Visual
                placedTowerTypeSO.prefab,
                // Position
                worldPosition,
                // Rotation
                Quaternion.Euler(0, 1, 0)
            );

        // get the instantiated/placed Tower
        PlacedTower placedTower = placedTowerTransform.GetComponent<PlacedTower>();

        placedTower.placedTowerTypeSO = placedTowerTypeSO;
        placedTower.origin = origin;
        // placedTower.dir = dir;

        return placedTower;
    }

    // Destroy the tower-visual
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
