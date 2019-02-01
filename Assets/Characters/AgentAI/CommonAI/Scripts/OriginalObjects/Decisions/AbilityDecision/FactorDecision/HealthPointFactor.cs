using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/HealthPointFactor")]
public class HealthPointFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return ((controller.healthPoints.currentHealthPoints / controller.healthPoints.maxHealthPoints) - 0.5f) * 100;
    }
}
