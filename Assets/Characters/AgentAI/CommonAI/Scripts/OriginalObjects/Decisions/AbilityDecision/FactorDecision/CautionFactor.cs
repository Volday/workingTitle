using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/CautionFactor")]
public class CautionFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return controller.AISkills.caution - 50;
    }
}
