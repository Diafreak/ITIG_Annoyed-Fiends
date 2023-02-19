using UnityEngine;


public class Projectile : MonoBehaviour {

    // current Target of the Projectile
    private Transform target;

    // Projectile Stats
    private float damage;
    private float damageRadius;
    private float speed;


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


    public void SetProjectileValues(float _damage, float _damageRadius, float _speed) {
        damage       = _damage;
        damageRadius = _damageRadius;
        speed        = _speed;
    }


    // seeking the current target
    public void Seek(Transform _target) {
        target = _target;
    }


    private void HitTarget() {

        if (damageRadius > 0f) {
            AoEDamage();
        } else {
            Damage(target);
        }

        Destroy(gameObject);
    }


    private void AoEDamage() {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (Collider hitObject in hitObjects) {
            if (hitObject.tag == "Enemy") {
                Damage(hitObject.transform);
            }
        }
    }


    private void Damage(Transform hitEnemy) {
        Enemy enemy = hitEnemy.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
