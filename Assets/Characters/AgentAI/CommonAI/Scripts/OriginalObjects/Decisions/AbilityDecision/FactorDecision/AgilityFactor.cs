using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/AgilityFactor")]
public class AgilityFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return controller.AISkills.agility - 50;
    }
}
