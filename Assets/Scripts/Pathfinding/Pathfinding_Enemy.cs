using UnityEngine;

public class Pathfinding_Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int hp = 100;

    public int killValue = 100;

    private Transform target;

    //the Waypoint that is currently targeted
    private int wayPointIndex = 0;


    void Start() {
        target = Waypoints.waypoints[0];
    }

    void Update () {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {
        //temporarily destroys enemy when reaches last waypoint
        if (wayPointIndex >= Waypoints.waypoints.Length - 1) 
        {
            FinishedPath();
            return;
        }

        wayPointIndex++;
        target = Waypoints.waypoints[wayPointIndex];
    }

    void FinishedPath()
    {
        Destroy(gameObject);
        PlayerStats.lives -= 1;
        townerSpawner.enemiesAlive--;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        
        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.money += killValue;
        townerSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}


