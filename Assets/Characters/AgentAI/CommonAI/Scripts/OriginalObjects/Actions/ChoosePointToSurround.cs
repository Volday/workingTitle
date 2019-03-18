using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToSurround")]
public class ChoosePointToSurround : ActionInState
{
    public override void Act(StateController controller)
    {
        LastStaps targetLastStaps = controller.targetToAttack.targetToAttack.GetComponent<LastStaps>();
        float distanceToTarget = MyMath.sqrDistanceFromPointToPoint(controller.targetToAttack.targetToAttack.transform.position, controller.transform.position);
        Vector3 targetFuturePosition = targetLastStaps.GetMotionVector(1);
        float distanceToTargetFuturePosition = MyMath.sqrDistanceFromPointToPoint(targetFuturePosition, controller.transform.position);
        if (distanceToTargetFuturePosition > distanceToTarget)
        {
            controller.pointToMove.pointToMove = targetFuturePosition;
        }
        else {
            controller.pointToMove.pointToMove = controller.targetToAttack.targetToAttack.transform.position;
        }
    }
}
