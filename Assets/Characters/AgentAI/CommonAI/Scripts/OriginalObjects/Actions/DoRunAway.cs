using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/DoRunAway")]
public class DoRunAway : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.targetToAttack.targetToAtack != null) {
            controller.navMeshAgent.destination = 2 * controller.transform.position - controller.targetToAttack.targetToAtack.transform.position;
        }
    }
}
