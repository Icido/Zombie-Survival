using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Action : ScriptableObject
{
    /*
     *the action scriptable object gives access to the function that will perform an ai method, for example roaming or chasing
     */
    public abstract void act(StateController controller);
}
