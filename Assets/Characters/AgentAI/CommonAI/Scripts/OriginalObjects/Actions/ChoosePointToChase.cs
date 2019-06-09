using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToChase")]
public class ChoosePointToChase : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack != null) {
            float myRadius = controller.gameObject.GetComponent<CapsuleCollider>().radius;
            float targetToAttackRadius = controller.targetToAttack.targetToAttack.GetComponent<CapsuleCollider>().radius;
            Vector3 vectorToTarget = controller.targetToAttack.targetToAttack.transform.position - controller.transform.position;
            float coefficient = (1 - (myRadius + targetToAttackRadius + 0.3f) / vectorToTarget.magnitude);
            vectorToTarget *= coefficient;
            vectorToTarget += controller.transform.position;
            controller.pointToMove.pointToMove = vectorToTarget;
        }
    }
}
