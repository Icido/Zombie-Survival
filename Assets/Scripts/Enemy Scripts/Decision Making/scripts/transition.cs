using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class transition
{
    /*
     * This class is a container for the decision class, and the actions it references
     * The decision will return true or false, and the next state will be chosen from the gievn trueState or falseState
     */
    public Descision decision;
    public State trueState;
    public State falseState;
}
