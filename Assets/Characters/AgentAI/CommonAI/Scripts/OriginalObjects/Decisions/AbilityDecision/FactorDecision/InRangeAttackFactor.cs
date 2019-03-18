using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/InRangeAttackFactor")]
public class InRangeAttackFactor : DecisionFactor
{
    //даёт +50 если стоишь близко к цели, и -50 если на границе радиуса атаки
    public override float GetDecisionFactor(StateController controller)
    {
        float distanceFactorValue = 0;
        float distanceToTarget = Mathf.Sqrt(MyMath.sqrDistanceFromPointToPoint(controller.targetToAttack.targetToAttack.transform.position, controller.transform.position));
        float rangeCast = controller.abilityPending.rangeCast;
        if (distanceToTarget > rangeCast)
        {
            distanceFactorValue = -50;
        }
        else
        {
            distanceFactorValue = ((1 - distanceToTarget / rangeCast) - 0.5f) * 100;
        }
        return distanceFactorValue;
    }
}
