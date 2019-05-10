using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    public float speed = 1f;

    AStar aStarPathfinding;

    float distanceToPlayer;

    //float distanceBetween;
    float distanceToNextNode;
    float nextNodeNear = 1f;
    int nodeNumber = 0;
    int maxNodeNum;

    List<Vector3> path = new List<Vector3>();
    Vector3 nextNode = new Vector3();
    public bool newDestination = false;

    public Vector3 destinationNode = new Vector3();

    private float minDistanceToPlayer = 1f;

    //private float distanceUntilChase = 10f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        aStarPathfinding = GetComponent<AStar>();
    }


    void Update()
    {

        if (newDestination)
        {
            aStarPathfinding.pathFind(transform.position, destinationNode);
            newDestination = false;
        }

        if (aStarPathfinding.finishedPath)
        {
            path = aStarPathfinding.storedPath;
            nodeNumber = 0;
            maxNodeNum = path.Count;
            if (path.Count > 0)
            {
                nextNode = path[nodeNumber];
                nextNode.y = 1f;
            }
        }

        distanceToNextNode = Vector3.Distance(transform.position, nextNode);
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToNextNode < nextNodeNear)
        {
            nodeNumber++;
            if (nodeNumber < maxNodeNum)
            {
                nextNode = path[nodeNumber];
                nextNode.y = 1f;
            }
            
            if(distanceToPlayer < minDistanceToPlayer)
            {
                nextNode = player.position;
                newDestination = false;
            }
        }

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            //Still alive and move
            transform.position = Vector3.MoveTowards(transform.position, nextNode, 0.05f);
        }
        else
        {
            //Stop moving
        }
    }
}
