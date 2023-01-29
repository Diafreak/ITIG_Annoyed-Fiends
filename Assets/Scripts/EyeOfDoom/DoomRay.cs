using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]

public class DoomRay : MonoBehaviour
{
    public float range = 100f;

    public Camera povCam;

    public float rayRange = 100f;

    public float rayDuration = 2f;

    public float fireRate = 1f;

    public Transform doomRayOrigin;

    public float damage = 50f;

    LineRenderer rayLine;
    float fireTimer;
    
    private void Awake() {

        rayLine = GetComponent<LineRenderer>();

    } 
    
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireTimer > fireRate)
        {
            fireTimer = 0;
            rayLine.SetPosition(0, doomRayOrigin.position);
            Vector3 rayOrigin = povCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, povCam.transform.forward, out hit, rayRange)){
                Debug.Log(hit.transform.name);
                rayLine.SetPosition(1, hit.point);
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null){
                    enemy.TakeDamage(damage);
                }
            }
            else{
                rayLine.SetPosition(1, rayOrigin+(povCam.transform.forward*rayRange));
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

//Ausblenden Mouse Plane, Fadenkreuz

