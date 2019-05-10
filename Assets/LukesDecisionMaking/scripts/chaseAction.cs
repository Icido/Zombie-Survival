using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAi/Actions/Chase")]
public class chaseAction : Action
{
    public override void act(StateController controller)
    {
        chase(controller);
    }

    private void chase(StateController controller)
    {
        controller.navMeshAgent.enabled = true;
        controller.currentObj.GetComponent<flock>().playerFound = true;
        controller.currentObj.GetComponent<Rigidbody>().isKinematic = true;
        controller.navMeshAgent.destination = controller.Player.transform.position;
    }
}
