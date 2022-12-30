using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float damageRadius = 0f;

    // seeking the current target
    public void Seek(Transform _target) {
        target = _target;
    }

    // Update is called once per frame
    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        // traveling the bullet to the target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World); 
        transform.LookAt(target);
    }

    void HitTarget() {
        Debug.Log("We Hit Something"); 
        
        if(damageRadius > 0f)
        {
            AoEDamage();
        }
        else
        {
            Damage(target);
        }
        
        
        Destroy(gameObject);
       
    }

    void AoEDamage()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider hitObject in hitObjects)
        {
            if(hitObject.tag == "Enemy")
            {
                Damage(hitObject.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    
}
