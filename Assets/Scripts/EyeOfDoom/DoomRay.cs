using System.Collections;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]

public class DoomRay : MonoBehaviour {

    [Header("Attributes")]
    public float rayRange = 100f;
    public float rayDuration = 2f;
    public float fireRate = 1f;
    public float damage = 50f;

    [Header("Camera")]
    public Camera povCam;

    [Header("Ray Origin")]
    public Transform doomRayOrigin;

    private LineRenderer rayLine;
    private float fireTimer;


    private void Awake() {
        rayLine = GetComponent<LineRenderer>();
    }


    void Update() {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer > fireRate) {
            fireTimer = 0;

            rayLine.SetPosition(0, doomRayOrigin.position);

            Vector3 rayOrigin = povCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(rayOrigin, povCam.transform.forward, out RaycastHit hit, rayRange)) {
                Debug.Log(hit.transform.name);
                rayLine.SetPosition(1, hit.point);

                Enemy enemy = hit.transform.GetComponent<Enemy>();

                if (enemy != null) {
                    enemy.TakeDamage(damage);
                }
            } else {
                rayLine.SetPosition(1, rayOrigin + (povCam.transform.forward * rayRange));
            }

            StartCoroutine(ShootDoomray());
        }
    }


    IEnumerator ShootDoomray() {
        rayLine.enabled = true;
        yield return new WaitForSeconds(rayDuration);
        rayLine.enabled = false;
    }
}