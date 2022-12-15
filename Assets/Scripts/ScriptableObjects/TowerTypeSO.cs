using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlacedTowerTypeSO", menuName = "ScriptableObjects/TowerTypeSO")]
public class TowerTypeSO : ScriptableObject {

    public string towerName;

    public Transform prefab;
    public Transform visual;
    public Sprite towerIcon;

    public float rotation;
    public float damage;
    public int level;
}
