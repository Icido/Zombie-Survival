using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/Decisions/Chase with attack")]
public class ChaseRangeSpecialDecision : Descision
{
    public override bool Decide(StateController controller)
    {
        bool canFollow = inRange(controller);
        return canFollow;
    }

    private bool inRange(StateController controller)
    {
        if (Vector3.Distance(controller.Player.transform.position, controller.currentObj.transform.position) < 30 && Vector3.Distance(controller.Player.transform.position, controller.currentObj.transform.position) > 5)
        {
            return true;
        }
        else if (Vector3.Distance(controller.Player.transform.position, controller.currentObj.transform.position) < 5)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
