using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlacedTowerTypeSO", menuName = "ScriptableObjects/PlacedTowerTypeSO")]
public class PlacedTowerTypeSO : ScriptableObject {

    public string towerName;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;

}
