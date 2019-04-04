using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;

    private WaveManagement wm;

    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManagement>();

        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        wm.zombieDeath(gameObject);

        StartSinking();
    }

    public void StartSinking()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }

}
