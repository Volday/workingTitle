using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/UnconditionalTransition")]
public class UnconditionalTransition : Decision
{
    public override float Decide(StateController controller)
    {
        return float.MaxValue;
    }
}
