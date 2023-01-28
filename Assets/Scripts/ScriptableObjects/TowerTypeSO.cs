using UnityEngine;


[CreateAssetMenu(fileName = "TowerTypeSO", menuName = "ScriptableObjects/TowerTypeSO")]
public class TowerTypeSO : ScriptableObject {

    [Header("Tower Name")]
    public string towerName;

    [Header("Tower Visuals")]
    public Transform prefab;
    public Transform visual;
    public GameObject projectilePrefab;

    [Header("Tower Stats")]
    public float range;
    public float fireRate;
    public float turnSpeed;
    public string enemyTag;

    [Header("Tower Prices")]
    public int price;
    public int upgradeCost;
    public int sellingPrice;

    [Header("Tower Level")]
    public int level;

    [Header("Gargoyle")]
    public float despawnTime = 0;
}
