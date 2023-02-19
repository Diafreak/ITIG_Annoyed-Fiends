using UnityEngine;


[CreateAssetMenu(fileName = "TowerTypeSO", menuName = "ScriptableObjects/TowerTypeSO")]
public class TowerTypeSO : ScriptableObject {

    [Header("Tower Name")]
    public string towerName;

    [Header("Tower Prefabs")]
    public Transform prefab;
    public GameObject projectilePrefab;

    [Header("Tower Stats")]
    public float range;
    public float fireRate;
    public float turnSpeed;
    public string enemyTag;

    [Header("Projectile")]
    public float projectileDamage;
    public float damageRadius;
    public float projectileSpeed;

    [Header("Tower Prices")]
    public int price;
    public int upgradeCost;
    public int sellingPrice;

    [Header("Tower Level")]
    public int level;

    [Header("Gargoyle")]
    public float despawnTime = 0;
}
