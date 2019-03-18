using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/RangeFactor")]
public class RangeFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        float distanceFactorValue = 0;
        Vector3 vectorToTarget = new Vector3(controller.targetToAttack.targetToAttack.transform.position.x - controller.transform.position.x,
            controller.targetToAttack.targetToAttack.transform.position.y - controller.transform.position.y,
            controller.targetToAttack.targetToAttack.transform.position.z - controller.transform.position.z);
        float distanceToTarget = ((vectorToTarget.x * vectorToTarget.x) + (vectorToTarget.y * vectorToTarget.y) + (vectorToTarget.z * vectorToTarget.z));

        float radiusOfView = controller.radiusOfView.radiusOfView * controller.radiusOfView.radiusOfView;
        float rangeCast = controller.abilityPending.rangeCast * controller.abilityPending.rangeCast;
        if (distanceToTarget < rangeCast || radiusOfView < rangeCast)
        {
            distanceFactorValue = 50;
        }
        else {
            distanceFactorValue = ((radiusOfView - distanceToTarget) / (radiusOfView - rangeCast) - 0.5f) * 100;
        }
        return distanceFactorValue;
    }
}
