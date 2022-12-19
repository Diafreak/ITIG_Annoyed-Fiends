using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TowerTypeSO", menuName = "ScriptableObjects/TowerTypeSO")]
public class TowerTypeSO : ScriptableObject {

    public string towerName;

    public Transform prefab;
    public Transform visual;
    public Sprite towerIcon;

    public float range;
    public float fireRate;
    public string enemyTag;

    public float turnSpeed;

    public GameObject projectilePrefab;
}
