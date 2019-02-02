using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/CooperationFactor")]
public class CooperationFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        return controller.AISkills.cooperation - 50;
    }
}
