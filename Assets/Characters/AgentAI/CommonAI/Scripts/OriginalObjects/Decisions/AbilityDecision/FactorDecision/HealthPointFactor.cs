using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/HealthPointFactor")]
public class HealthPointFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        if (controller.healthPoints.maxHealthPoints != 0)
        {
            return ((controller.healthPoints.currentHealthPoints / controller.healthPoints.maxHealthPoints) - 0.5f) * 100;
        }
        else {
            return -50;
        }
    }
}
