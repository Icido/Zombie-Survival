﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/Actions/Patrol")]
public class PatrolAction : Action
{
    public Transform currentPosition;
    Vector3 targetDest;
    public int x1, x2, z1, z2;
    bool atDest = false;
    public override void act(StateController controller)
    {
        currentPosition = controller.currentObj.transform;
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        //controller.currentObj.GetComponent<flock>().playerFound = false;

        //int xPos = Random.Range(x1, x2);
        //int zPos = Random.Range(z1, z2);

        //Vector3 newPosition = new Vector3(currentPosition.transform.position.x + xPos, currentPosition.transform.position.y, currentPosition.transform.position.z + zPos);

        //if (controller.currentObj.transform.position == targetDest)
        //{
        //    atDest = true;
        //}
        //if (atDest)
        //{
            
        //    controller.currentObj.GetComponent<EnemyMovement>().destinationNode = newPosition;
        //        targetDest = newPosition;
        //    atDest = false;
        controller.currentObj.GetComponent<flock>().playerFound = false;
        controller.currentObj.GetComponent<EnemyMovement>().enabled = false;
        controller.currentObj.GetComponent<flock>().isFlocking = true;
        //}
        
        //controller.navMeshAgent.destination = newPosition;

        

    }
}
