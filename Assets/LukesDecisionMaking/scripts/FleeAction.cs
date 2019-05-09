using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAi/Actions/Flee")]
public class FleeAction : Action
{
    public override void act(StateController controller)
    {
        Vector3 directionVector = controller.Player.transform.position - controller.currentObj.transform.position;
        directionVector *= 10;
        controller.navMeshAgent.destination = controller.currentObj.transform.position + directionVector;

    }
}
