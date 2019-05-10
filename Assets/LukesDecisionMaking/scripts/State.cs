using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAi/State")]
public class State : ScriptableObject
{
    /*
     * This scriptable object holds the actions and transitions for the current state
     */

    public Action[] actions;
    public transition[] transitions;
    public void updateState(StateController controller)
    {
        doActions(controller);
        checkTransitions(controller);
    }

    private void doActions(StateController controller) //This function iterates over the array of actions, and performs the function of each action
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].act(controller);
        }
    }

    private void checkTransitions(StateController controller) //This function iterates over the array of transitions, this will return the next state that the ai will use
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
