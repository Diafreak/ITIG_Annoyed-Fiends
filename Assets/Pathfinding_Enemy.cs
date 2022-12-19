
using UnityEngine;

public class Pathfinding_Enemy : MonoBehaviour
{

    public float speed = 10f;


    private Transform target;

    //the waypoint that is currently targetet
    private int wayepointIndex = 0;


    void start()
    {
        target = Waypoints.waypoints[0];

    }

    void update ()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    
        
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)

        {
            GetNextWaypoint();
        }
    
    }


    void GetNextWaypoint()
    {
        //temporary destroys enemy when reaches last waypoint
        if (wayepointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
        }
        wayepointIndex++;
        target = Waypoints.waypoints[wayepointIndex];
    }


}


