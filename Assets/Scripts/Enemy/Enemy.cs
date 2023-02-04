using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    public float startHP = 100;
    public int killValue = 100;
    public float speed = 10f;

    [Header("Health Bar Visual")]
    public Image healthBar;

    private float currentHP;

    private bool isBlocked;

    // Pathfinding
    private Transform nextWaypoint;
    private int nextWaypointIndex = 0;



    private void Start() {
        nextWaypoint = Waypoints.waypoints[0];
        speed = gameObject.GetComponent<Enemy>().speed;
        isBlocked = false;
        currentHP = startHP;
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
        healthBar.fillAmount = currentHP / startHP;

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
