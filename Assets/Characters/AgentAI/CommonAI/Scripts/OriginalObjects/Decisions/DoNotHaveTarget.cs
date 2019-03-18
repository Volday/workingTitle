using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DoNotHaveTarget")]
public class DoNotHaveTarget : Decision
{
    public override float Decide(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack == null)
        {
            return float.MaxValue;
        }
        return float.MinValue;
    }
}
