using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/Actions/Patrol")]
public class patrolAction : Action
{
    public Transform currentPosition;
    public int x1, x2, z1, z2;
    public override void act(StateController controller)
    {
        currentPosition = controller.currentObj.transform;
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.currentObj.GetComponent<flock>().playerFound = false;

        int xPos = Random.Range(x1, x2);
        int zPos = Random.Range(z1, z2);

        Vector3 newPosition = new Vector3(currentPosition.transform.position.x + xPos, currentPosition.transform.position.y, currentPosition.transform.position.z + zPos);

        controller.navMeshAgent.destination = newPosition;

        controller.currentObj.GetComponent<flock>().isFlocking = true;

    }
}
