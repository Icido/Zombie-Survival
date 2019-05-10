using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAi/Decisions/Flee")]
public class FleeDecision : Descision
{
    public override bool Decide(StateController controller)
    {
        bool canFollow = inRange(controller);
        return canFollow;
    }

    private bool inRange(StateController controller)
    {
        if (controller.health < 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
