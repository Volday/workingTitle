using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/CooldownFactor")]
public class CooldownFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        float cooldownFactorValue = 0;
        if (controller.abilityPending.cooldown < controller.abilityPending.timeAfterLastCast) {
            cooldownFactorValue = 50;
        }
        else {
            cooldownFactorValue = ((controller.abilityPending.timeAfterLastCast / controller.abilityPending.cooldown) - 0.5f) * 100;
        }
        return cooldownFactorValue;
    }
}
