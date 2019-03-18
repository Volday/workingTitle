using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/MoveToTarget")]
public class MoveToTarget : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack != null) {
            controller.navMeshAgent.destination = controller.targetToAttack.targetToAttack.transform.position;
        }
    }
}
