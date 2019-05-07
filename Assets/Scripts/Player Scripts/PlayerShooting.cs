using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public float accuracy = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }

        Debug.Log("Player Still shooting");

    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }




}
