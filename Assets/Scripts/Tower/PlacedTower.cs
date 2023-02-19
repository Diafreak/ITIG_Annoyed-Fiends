using UnityEngine;


public class PlacedTower : MonoBehaviour {

    // current Target of the Tower
    private Transform target;

    // Attributes
    private string towerName;
    private float range;
    private float fireRate;
    private float fireCountdown = 0f;
    private float projectileDamage;
    private float damageRadius;
    private float projectileSpeed;

    // Tag to find Enemies
    private string enemyTag;

    // Upgrading
    private int level;
    private int upgradeCost;
    private int sellingPrice;

    // Rotation
    [SerializeField] private Transform partToRotate;
    private float turnSpeed;

    // Shooting
    private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    // Gargoyle
    private float despawnTime;

    // Blocked Enemy List from Gargoyle
    private Collider[] blockedEnemies;

    private GridBuildingSystem gridBuildingSystem;
    private TowerShop towerShop;


    private void Start() {
        gridBuildingSystem = GridBuildingSystem.instance;
        towerShop = TowerShop.instance;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    // create a Tower on the clicked position
    public static PlacedTower Create(Vector3 worldPosition, TowerTypeSO towerTypeSO, float yOffset) {

        if (towerTypeSO.towerName != "Gargoyle") {
            yOffset = 0f;
        }

        Transform placedTowerTransform =
            Instantiate(
                // Visual
                towerTypeSO.prefab,
                // Position
                worldPosition + new Vector3(0, yOffset, 0),
                // Rotation
                Quaternion.identity
            );

        // get the instantiated/placed Tower
        PlacedTower placedTower = placedTowerTransform.GetComponent<PlacedTower>();

        // set all variables from the template to the placed Tower
        placedTower.towerName        = towerTypeSO.name;
        placedTower.range            = towerTypeSO.range;
        placedTower.fireRate         = towerTypeSO.fireRate;
        placedTower.projectileDamage = towerTypeSO.projectileDamage;
        placedTower.damageRadius     = towerTypeSO.damageRadius;
        placedTower.projectileSpeed  = towerTypeSO.projectileSpeed;
        placedTower.enemyTag         = towerTypeSO.enemyTag;
        placedTower.level            = towerTypeSO.level;
        placedTower.upgradeCost      = towerTypeSO.upgradeCost;
        placedTower.sellingPrice     = towerTypeSO.sellingPrice;
        placedTower.turnSpeed        = towerTypeSO.turnSpeed;
        placedTower.projectilePrefab = towerTypeSO.projectilePrefab;
        placedTower.despawnTime      = towerTypeSO.despawnTime;

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

    public float GetRange() {
        return range;
    }



    // ------------------------------
    // Upgrade
    // ------------------------------

    public void UpgradeTower() {

        if (!PlayerHasEnoughMoney()) {
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

    private bool PlayerHasEnoughMoney() {
        return PlayerStats.GetMoney() >= upgradeCost;
    }



    // ------------------------------
    // Selling / Destroying
    // ------------------------------

    public void SellTower() {
        PlayerStats.AddMoney(sellingPrice);
        DestroySelf();
    }


    // Destroy the tower-visual
    private void DestroySelf() {
        Destroy(gameObject, despawnTime);
    }


    private void OnDestroy() {
        if (towerName != "Gargoyle") {
            return;
        }

        UnblockEnemies();
        // reactivate the Placement-Tile on the Gargoyle's Position
        GridObject gargoyle = gridBuildingSystem.GetGridXZ().GetGridObject(transform.position);
        gridBuildingSystem.ReactivateGridTile(gargoyle.GetGridPosition().x, gargoyle.GetGridPosition().z);
    }



    // ------------------------------
    // Gargoyle
    // ------------------------------

    private void UnblockEnemies() {
        foreach (Collider enemy in blockedEnemies) {
            if (enemy.tag == enemyTag) {
                enemy.gameObject.GetComponent<Enemy>().UnblockEnemy();
            }
        }
    }


    private void BlockEnemies() {
        blockedEnemies = Physics.OverlapSphere(transform.position + gridBuildingSystem.GetBuildOffset(), gridBuildingSystem.GetCellSize()/2);

        foreach (Collider enemy in blockedEnemies) {
            if (enemy.tag == enemyTag) {
                enemy.transform.GetComponent<Enemy>().BlockEnemy();
            }
        }
    }



    // ------------------------------
    // Shooting / Blocking
    // ------------------------------

    private void Update() {

        if (towerName == "Gargoyle") {
            towerShop.LockGargoyle();
            BlockEnemies();
            DestroySelf();
            return;
        }

        if (target == null) {
            return;
        }

        LockOnTarget();

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

        target = null;

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
    }


    private void LockOnTarget() {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    // shooting at the designated target
    private void Shoot() {
        GameObject projectileGO = (GameObject) Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.SetProjectileValues(projectileDamage, damageRadius, projectileSpeed);

        if (projectile != null) {
            projectile.Seek(target);
        }
    }



    // ------------------------------
    // Debug
    // ------------------------------

    // drawing a red gizmo around the selected tower that indicates the towers range
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + gridBuildingSystem.GetBuildOffset(), range);
    }
}