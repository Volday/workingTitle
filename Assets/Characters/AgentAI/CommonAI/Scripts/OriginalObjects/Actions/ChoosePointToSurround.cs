using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToSurround")]
public class ChoosePointToSurround : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.nextUnitAbility != null)
        {
            Surrounding surrounding = controller.GetComponent<Surrounding>();
            if (MyMath.sqrDistanceFromPointToPoint(controller.transform.position, controller.targetToAttack.targetToAttack.transform.position) >
                controller.nextUnitAbility.rangeCast * controller.nextUnitAbility.rangeCast ||
                surrounding == null)
            {
                if (surrounding != null)
                {
                    Destroy(controller.GetComponent<Surrounding>());
                }
                if (controller.targetToSurround != controller.targetToAttack.targetToAttack && controller.targetToSurround != null)
                {
                    controller.targetToSurround.GetComponent<Surround>().RemoveSurround(controller.gameObject);
                }
                controller.targetToSurround = controller.targetToAttack.targetToAttack;
                controller.gameObject.AddComponent<Surrounding>();
                Surround targetSurround = controller.targetToAttack.targetToAttack.GetComponent<Surround>();
                controller.pointToMove.pointToMove = targetSurround.GetPositionToSurround(controller.gameObject, controller.nextUnitAbility.rangeCast);
            }
            else
            {
                if (surrounding != null && surrounding.CheckForUpdatePisition(controller.targetToAttack.targetToAttack))
                {
                    if (controller.targetToSurround != controller.targetToAttack.targetToAttack && controller.targetToSurround != null)
                    {
                        controller.targetToSurround.GetComponent<Surround>().RemoveSurround(controller.gameObject);
                    }
                    controller.targetToSurround = controller.targetToAttack.targetToAttack;
                    Surround targetSurround = controller.targetToAttack.targetToAttack.GetComponent<Surround>();
                    controller.pointToMove.pointToMove = targetSurround.GetPositionToSurround(controller.gameObject, controller.nextUnitAbility.rangeCast);
                }
            }
        }
        else {
            controller.pointToMove.pointToMove = controller.transform.position;
        }
    }
}
