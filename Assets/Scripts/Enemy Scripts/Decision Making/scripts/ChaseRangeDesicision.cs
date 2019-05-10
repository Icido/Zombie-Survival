using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/Decisions/ChaseRange")]
public class ChaseRangeDesicision : Descision
{
    float playerRange = 25f;

    public override bool Decide(StateController controller)
    {
        bool canFollow = inRange(controller);
        return canFollow;
    }

    private bool inRange(StateController controller)
    {
        flock currentObjFlock = controller.currentObj.GetComponent<flock>();

        List<GameObject> neighbors = controller.waveManager.GetComponent<levelController>().getNeighbours(controller.currentObj, currentObjFlock.neighbourRadius);
        foreach (GameObject n in neighbors)
        {
            if (n.GetComponent<flock>().playerFound == true)
            {
                controller.currentObj.GetComponent<flock>().isFlocking = false;
                break;
            }
        }
        if (Vector3.Distance(controller.Player.transform.position, controller.currentObj.transform.position) < playerRange)//add: or if zomb in neighborhood = player found
            return true;
        else
            return false;
    }
}
