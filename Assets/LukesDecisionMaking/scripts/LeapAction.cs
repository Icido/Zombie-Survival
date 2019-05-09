using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAi/Actions/Leap")]
public class LeapAction : Action
{
    public override void act(StateController controller)
    {
        if (controller.canAttack)
        {
            Vector3 currentVel = controller.navMeshAgent.velocity;
            Debug.Log(currentVel);
            //controller.navMeshAgent.enabled = false;
            //controller.currentObj.GetComponent<Rigidbody>().isKinematic = false;
            //controller.currentObj.GetComponent<Rigidbody>().AddForce(currentVel * 25 + Vector3.up * 25);
            //controller.navMeshAgent.Move(currentVel * 25 + Vector3.up * 25);
            controller.navMeshAgent.velocity = controller.navMeshAgent.velocity + new Vector3(0, 10, 0);
            Debug.Log("here");
            controller.canAttack = false;
        }
        else
        {
            controller.navMeshAgent.SetDestination(controller.Player.transform.position);
        }

    }
}
