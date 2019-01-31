using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/MoveToTarget")]
public class MoveToTarget : ActionInState
{
    public override void Act(StateController controller)
    {
        controller.navMeshAgent.destination = controller.targetToAttack.targetToAtack.transform.position;
    }
}
