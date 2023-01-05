using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedTower : MonoBehaviour
{
    // Reference to the Tower-SO
    private TowerTypeSO towerTypeSO;

    public static PlacedTower Create(Vector3 worldPosition, TowerTypeSO placedTowerTypeSO) {
        // create a Tower on the clicked position
        Transform placedTowerTransform =
            Instantiate(
                // Visual
                placedTowerTypeSO.prefab,
                // Position
                worldPosition,
                // Rotation
                Quaternion.Euler(0, 0, 0)
            );
        // get the instantiated/placed Tower
        PlacedTower placedTower = placedTowerTransform.GetComponent<PlacedTower>();

        placedTower.towerTypeSO = placedTowerTypeSO;

        placedTower.range = placedTowerTypeSO.range;
        placedTower.fireRate = placedTowerTypeSO.fireRate;
        placedTower.enemyTag = placedTowerTypeSO.enemyTag;
        placedTower.turnSpeed = placedTowerTypeSO.turnSpeed;
        placedTower.projectilePrefab = placedTowerTypeSO.projectilePrefab;

        return placedTower;
    }

    // Destroy the tower-visual
    public void DestroySelf() {
        Destroy(gameObject);
    }

    // ------------------------------
    // ------------------------------
    // ------------------------------

    // the current target of the tower
    private Transform target;

    // Attributes
    private float range;
    private float fireRate;
    private float fireCountdown = 0f;

    // the tag to find the enemies
    private string enemyTag;
    // variables for rotation
    [SerializeField] private Transform partToRotate;
    private float turnSpeed;

    private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // finding the closest enemy in range
    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position + new Vector3(10/2, 0, 10/2), enemy.transform.position);    // replace with offset
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }
    }


    // Update is called once per frame
    void Update() {
        if (target == null) {
            return;
        }

        // target lock on
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f) {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    // shooting at the designated target
    void Shoot() {
        Debug.Log("SHOOT!");
        GameObject projectileGO = (GameObject) Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null) {
            projectile.Seek(target);
        }
    }

    // drawing a red gizmo around the selected tower that indicates the towers range
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position + new Vector3(10/2, 0, 10/2), range);  //Vector3 offset = new Vector3(GetComponent<GridBuildingSystem>().gridWidth/2, 0, GetComponent<GridBuildingSystem>().gridHeight/2);
    }
}
