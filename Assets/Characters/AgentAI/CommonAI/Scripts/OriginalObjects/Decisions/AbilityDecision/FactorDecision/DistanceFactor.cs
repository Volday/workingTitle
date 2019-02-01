using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/DistanceFactor")]
public class DistanceFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        float distanceFactorValue = 0;
        Vector3 vectorToTarget = new Vector3(controller.targetToAttack.targetToAtack.transform.position.x - controller.transform.position.x,
            controller.targetToAttack.targetToAtack.transform.position.y - controller.transform.position.y,
            controller.targetToAttack.targetToAtack.transform.position.z - controller.transform.position.z);
        float distanceToTarget = ((vectorToTarget.x * vectorToTarget.x) + (vectorToTarget.y * vectorToTarget.y) + (vectorToTarget.z * vectorToTarget.z));

        float radiusOfView = controller.radiusOfView.radiusOfView * controller.radiusOfView.radiusOfView;
        float rangeCast = controller.abilityPending.rangeCast * controller.abilityPending.rangeCast;
        if (distanceToTarget < rangeCast)
        {
            distanceFactorValue = 50;
        }
        else {
            distanceFactorValue = ((radiusOfView - rangeCast) / (radiusOfView - distanceToTarget) - 0.5f) * 100;
        }
        return distanceFactorValue;
    }
}
