using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    [Header("Health Bar Visual")]
    public Image healthBar;

    // Attributes
    private string enemyName;
    private float startHp;
    private int killValue;
    private float speed;

    private float currentHP;

    private bool isBlocked;

    // Pathfinding
    private Transform nextWaypoint;
    private int nextWaypointIndex = 0;


    public static Enemy Create(Vector3 worldPosition, EnemyTypeSO enemyTypeSO) {

        Transform spawnedEnemyTransform =
            Instantiate(
                // Visual
                enemyTypeSO.prefab,
                // Position
                worldPosition,
                // Rotation
                Quaternion.identity
            );

        // get the spawned Enemy
        Enemy enemy = spawnedEnemyTransform.GetComponent<Enemy>();

        // set variables from the template to the spawned Enemy
        enemy.enemyName = enemyTypeSO.enemyName;
        enemy.startHp   = enemyTypeSO.startHp;
        enemy.killValue = enemyTypeSO.killValue;
        enemy.speed     = enemyTypeSO.speed;

        return enemy;
    }


    private void Start() {
        nextWaypoint = Waypoints.waypoints[0];
        speed = gameObject.GetComponent<Enemy>().speed;
        isBlocked = false;
        currentHP = startHp;
    }


    void Update () {
        if (isBlocked) {
            return;
        }

        MoveToNextWaypoint();

        if (Vector3.Distance(transform.position, nextWaypoint.position) <= 1f) {
            GetNextWaypoint();
        }
    }



    // ------------------------------
    // Taking Damage
    // ------------------------------

    public void TakeDamage(float amount) {
        currentHP -= amount;
        healthBar.fillAmount = currentHP / startHp;

        if (currentHP <= 0) {
            Die();
        }
    }


    private void Die() {
        PlayerStats.AddMoney(killValue);
        EnemySpawner.enemiesAlive--;
        Destroy(gameObject);
    }



    // ------------------------------
    // Pathfinding
    // ------------------------------

    private void MoveToNextWaypoint() {
        Vector3 direction = nextWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }


    void GetNextWaypoint() {
        if (nextWaypointIndex >= Waypoints.waypoints.Length - 1) {
            PathIsFinished();
            return;
        }

        nextWaypointIndex++;
        nextWaypoint = Waypoints.waypoints[nextWaypointIndex];
    }


    void PathIsFinished() {
        Destroy(gameObject);

        if (PlayerStats.lives != 0) {
            PlayerStats.lives -= 1;
        }

        EnemySpawner.enemiesAlive--;
    }


    public void BlockEnemy() {
        isBlocked = true;
    }


    public void UnblockEnemy() {
        isBlocked = false;
    }
}
