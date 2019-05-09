using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10;
    public float range = 0.5f;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    EnemyHatChange hatChanger;

    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        hatChanger = GetComponent<EnemyHatChange>();

        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            playerInRange = true;
            hatChanger.changeHat(0);
        }
        else
        {
            playerInRange = false;
            hatChanger.changeHat(2);
        }

        //TODO: DECISION MAKING
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            //PLAYER DEAD
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        hatChanger.changeHat(1);

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
