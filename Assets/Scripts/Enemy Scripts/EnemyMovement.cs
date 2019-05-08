using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    public float speed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Update()
    {
        //TODO: PATHFINDING

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            //Still alive and move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.001f);
        }
        else
        {
            //Stop moving
        }
    }
}
