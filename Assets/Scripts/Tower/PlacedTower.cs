using UnityEngine;


public class PlacedTower : MonoBehaviour {

    // current target of the tower
    private Transform target;

    // Attributes
    private string towerName;
    private float range;
    private float fireRate;
    private float fireCountdown = 0f;

    // tag to find the enemies
    private string enemyTag;

    // Upgrading
    private int level;
    private int upgradeCost;
    private int sellingPrice;

    // variables for rotation
    [SerializeField] private Transform partToRotate;
    private float turnSpeed;

    // shooting
    private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    // Blocked Enemy List from Gargoyle
    private Collider[] blockedEnemies;

    private GridBuildingSystem gridBuildingSystem;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    // create a Tower on the clicked position
    public static PlacedTower Create(Vector3 worldPosition, TowerTypeSO towerTypeSO) {

        Transform placedTowerTransform =
            Instantiate(
                // Visual
                towerTypeSO.prefab,
                // Position
                worldPosition,
                // Rotation
                Quaternion.identity
            );

        // get the instantiated/placed Tower
        PlacedTower placedTower = placedTowerTransform.GetComponent<PlacedTower>();

        // set all variables from the template to the placed Tower
        placedTower.towerName        = towerTypeSO.name;
        placedTower.range            = towerTypeSO.range;
        placedTower.fireRate         = towerTypeSO.fireRate;
        placedTower.enemyTag         = towerTypeSO.enemyTag;
        placedTower.level            = towerTypeSO.level;
        placedTower.upgradeCost      = towerTypeSO.upgradeCost;
        placedTower.sellingPrice     = towerTypeSO.sellingPrice;
        placedTower.turnSpeed        = towerTypeSO.turnSpeed;
        placedTower.projectilePrefab = towerTypeSO.projectilePrefab;

        return placedTower;
    }


    public int GetUpgradeCost() {
        return upgradeCost;
    }

    public int GetLevel() {
        return level;
    }

    public int GetSellingPrice() {
        return sellingPrice;
    }


    // ------------------------------
    // Upgrade
    // ------------------------------

    public void UpgradeTower() {

        if (PlayerStats.GetMoney() < upgradeCost) {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        PlayerStats.SubtractMoney(upgradeCost);
        IncreaseLevel(1);
        IncreaseUpgradeCost(20);
        IncreaseFireRate(0.5f);
        IncreaseRange(1.5f);
        IncreaseSellingPrice(10);
        Debug.Log("Upgraded!");
    }

    private void IncreaseLevel(int increase) {
        level += increase;
    }

    private void IncreaseUpgradeCost(int increase) {
        upgradeCost += increase;
    }

    private void IncreaseFireRate(float increase) {
        fireRate += increase;
    }

    private void IncreaseRange(float increase) {
        range += increase;
    }

    private void IncreaseSellingPrice(int increase) {
        sellingPrice += increase;
    }


    // ------------------------------
    // Selling
    // ------------------------------

    public void SellTower() {
        PlayerStats.AddMoney(sellingPrice);
        DestroySelf();
    }

    // Destroy the tower-visual
    private void DestroySelf() {
        Destroy(gameObject);
    }

    public void SelfDestruct() {
        Destroy(gameObject, 5);
    }

    private void OnDestroy() {
        if (towerName == "Gargoyle") {
            foreach (Collider enemy in blockedEnemies) {
                if (enemy.tag == enemyTag)
                enemy.gameObject.GetComponent<Pathfinding>().UnblockEnemy();
            }
        }
    }


    // ------------------------------
    // Shooting
    // ------------------------------

    private void Update() {

        if (towerName == "Gargoyle") {
            blockedEnemies = Physics.OverlapSphere(transform.position + gridBuildingSystem.GetBuildOffset(), 5);
            foreach (Collider enemy in blockedEnemies) {
                if (enemy.tag == enemyTag) {
                    enemy.transform.GetComponent<Pathfinding>().BlockEnemy();
                }
            }
            SelfDestruct();
            return;
        }

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


    // finding the closest enemy in range
    private void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position + gridBuildingSystem.GetBuildOffset(), enemy.transform.position);
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


    // shooting at the designated target
    private void Shoot() {
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
        Gizmos.DrawWireSphere(transform.position + gridBuildingSystem.GetBuildOffset(), range);
    }
}