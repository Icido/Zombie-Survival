using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Descision : ScriptableObject
{
    /*
     * This Scriptable Object holds the function that will determine a decision. This returns a bool, so that an option for true or an option for false can be decided.
     * To expand on this, a different method would need to be created to allow for a non binary solution
     */
    public abstract bool Decide(StateController controller);
}
