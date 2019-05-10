using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAi/Actions/Chase")]
public class ChaseAction : Action
{
    public override void act(StateController controller)
    {
        chase(controller);
    }

    private void chase(StateController controller)
    {
        controller.currentObj.GetComponent<flock>().isFlocking = false;
        controller.currentObj.GetComponent<EnemyMovement>().enabled = true;

        controller.currentObj.GetComponent<flock>().playerFound = true;
        controller.currentObj.GetComponent<Rigidbody>().isKinematic = true;

        controller.currentObj.GetComponent<EnemyMovement>().destinationNode = controller.Player.transform.position;
        controller.currentObj.GetComponent<EnemyMovement>().newDestination = true;
    }
}
