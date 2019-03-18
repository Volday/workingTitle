using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/DistanceFactor")]
public class DistanceFactor : DecisionFactor
{
    //возвращает 50 если близко и -50 если на границе обзора
    public override float GetDecisionFactor(StateController controller)
    {
        float distanceFactorValue = 0;
        Vector3 vectorToTarget = new Vector3(controller.targetToAttack.targetToAttack.transform.position.x - controller.transform.position.x,
            controller.targetToAttack.targetToAttack.transform.position.y - controller.transform.position.y,
            controller.targetToAttack.targetToAttack.transform.position.z - controller.transform.position.z);
        float distanceToTarget = ((vectorToTarget.x * vectorToTarget.x) + (vectorToTarget.y * vectorToTarget.y) + (vectorToTarget.z * vectorToTarget.z));

        float radiusOfView = controller.radiusOfView.baseRadiusOfView * controller.radiusOfView.baseRadiusOfView;
        if (distanceToTarget > radiusOfView)
        {
            distanceFactorValue = -50;
        }
        else
        {
            distanceFactorValue = ((1 - distanceToTarget / radiusOfView) - 0.5f) * 100;
        }
        return distanceFactorValue;
    }
}
