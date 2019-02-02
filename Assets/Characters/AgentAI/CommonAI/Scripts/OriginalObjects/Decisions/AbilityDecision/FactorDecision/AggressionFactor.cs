using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/AggressionFactor")]
public class AggressionFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return controller.AISkills.aggression - 50;
    }
}
