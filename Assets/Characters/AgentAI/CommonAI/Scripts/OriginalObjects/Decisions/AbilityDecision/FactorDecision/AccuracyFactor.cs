using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/AccuracyFactor")]
public class AccuracyFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return controller.AISkills.accuracy - 50;
    }
}
