using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/FromEvasion")]
public class FromEvasion : Decision
{
    public override float Decide(StateController controller)
    {
        if (controller.GetComponent<NowEvasion>() == null) {
            return float.MaxValue;
        }
        return float.MinValue;
    }
}
