using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/State")]
public class State : ScriptableObject
{

    public Action[] actions;
    public transition[] transitions;
    public void updateState(StateController controller)
    {
        doActions(controller);
        checkTransitions(controller);
    }

    private void doActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].act(controller);
        }
    }

    private void checkTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSuccess = transitions[i].decision.Decide(controller);

            if (decisionSuccess == true)
            {
                controller.stateTransition(transitions[i].trueState);
            }
            else
            {
                controller.stateTransition(transitions[i].falseState);
            }
        }
    }

}
