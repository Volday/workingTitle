using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/NotMeleeAbility")]
public class NotMeleeAbility : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        if (controller.nextUnitAbility != null && controller.nextUnitAbility.rangeCast > 5)
        {
            return 0;
        }
        else {
            return float.MinValue;
        }
    }
}
