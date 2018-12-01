using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "AgentAI/CommonAI/Actions")]
public abstract class Action : ScriptableObject
{
    public abstract void Act(StateController controller);
}
