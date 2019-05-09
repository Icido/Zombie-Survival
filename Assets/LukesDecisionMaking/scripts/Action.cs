using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Action : ScriptableObject
{
    public abstract void act(StateController controller);
}
