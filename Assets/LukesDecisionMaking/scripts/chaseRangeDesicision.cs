using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/Decisions/ChaseRange")]
public class chaseRangeDesicision : Descision
{
    public override bool Decide(StateController controller)
    {
        bool canFollow = inRange(controller);
        return canFollow;
    }

    private bool inRange(StateController controller)
    {
        if (Vector3.Distance(controller.Player.transform.position, controller.currentObj.transform.position) < 30)
            return true;
        else
            return false;
    }
}
