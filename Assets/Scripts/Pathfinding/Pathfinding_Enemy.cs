using UnityEngine;
using UnityEngine.UI;
 
public class Pathfinding_Enemy : MonoBehaviour
{
    public float speed = 10f;

    public float startHp = 100;
    float hp;

    public int killValue = 100;

    public Image healthBar;

    private Transform target;

    //the Waypoint that is currently targeted
    private int wayPointIndex = 0;

    void Start() {
        target = Waypoints.waypoints[0];
        hp = startHp;
    }

    void Update () {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }


    void GetNextWaypoint() {
        if (wayPointIndex >= Waypoints.waypoints.Length - 1) {
            FinishedPath();
            return;
        }

        wayPointIndex++;
        target = Waypoints.waypoints[wayPointIndex];
    }

    void FinishedPath() {
        Destroy(gameObject);

        if (PlayerStats.lives != 0) 
        {
            PlayerStats.lives -= 1;
        }
        townerSpawner.enemiesAlive--;
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        healthBar.fillAmount = hp / startHp;
        
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die() {
        PlayerStats.money += killValue;
        townerSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}


