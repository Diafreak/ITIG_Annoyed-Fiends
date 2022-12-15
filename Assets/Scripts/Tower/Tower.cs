using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // the current target of the tower
    private Transform target;

    // the maximum range of the turret
    public float range = 15f;

    // the tag to find the enemies
    public string enemyTag = "Enemy";

    // variables for rotation
    public Transform partToRotate;
    public float turnSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // finding the closest enemy in range
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else{
            target = null;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            return;
        }

        // target lock on
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // drawing a red gizmo around the selected tower that indicates the towers range
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
