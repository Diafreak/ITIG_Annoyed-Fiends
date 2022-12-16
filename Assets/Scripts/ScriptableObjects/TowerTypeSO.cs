using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerTypeSO", menuName = "ScriptableObjects/TowerTypeSO")]
public class TowerTypeSO : ScriptableObject {

    public string towerName;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;

}
