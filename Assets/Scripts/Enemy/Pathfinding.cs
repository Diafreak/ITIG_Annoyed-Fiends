using UnityEngine;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour {

    private float speed;

    private Transform target;

    //the Waypoint that is currently targeted
    private int wayPointIndex = 0;


    void Start() {
        target = Waypoints.waypoints[0];
        speed = gameObject.GetComponent<Enemy>().speed;
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

        if (PlayerStats.lives != 0) {
            PlayerStats.lives -= 1;
        }

        EnemySpawner.enemiesAlive--;
    }
}


