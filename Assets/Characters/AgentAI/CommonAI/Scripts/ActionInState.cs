using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionInState : ScriptableObject
{
    public abstract void Act(StateController controller);
}
