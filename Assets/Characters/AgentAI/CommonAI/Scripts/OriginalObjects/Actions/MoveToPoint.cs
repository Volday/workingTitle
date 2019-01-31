using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/MoveToPoint")]
public class MoveToPoint : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.pointToMove.pointToMove != null) {
            controller.navMeshAgent.destination = controller.pointToMove.pointToMove;
        }
    }
}
