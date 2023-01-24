using UnityEngine;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour {

    private float speed;

    private Transform target;

    //the Waypoint that is currently targeted
    private int wayPointIndex = 0;

    private bool isBlocked;

    void Start() {
        target = Waypoints.waypoints[0];
        speed = gameObject.GetComponent<Enemy>().speed;
        isBlocked = false;
    }

    void Update () {
        if (!isBlocked) {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
                GetNextWaypoint();
            }
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


    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collided with Tower");
        //other.gameObject.GetComponent<PlacedTower>().SelfDestruct();
        //isBlocked = true;
    }

    public void BlockEnemy() {
        isBlocked = true;
    }

    public void UnblockEnemy() {
        isBlocked = false;
    }
}