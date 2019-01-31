using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "PluggableAI/Decision/Decisions")]
public abstract class Decision : ScriptableObject
{
    public abstract float Decide(StateController controller);
}
