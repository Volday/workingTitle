using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToChase")]
public class ChoosePointToChase : ActionInState
{
    public override void Act(StateController controller)
    {
        Vector3 target = controller.transform.position;
        if (controller.targetToAttack.targetToAttack != null) {
            target = controller.targetToAttack.targetToAttack.transform.position;
        }
        float distanceToTarget = MyMath.sqrDistanceFromPointToPoint(target, controller.transform.position);
        Ray ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit = new RaycastHit();
        bool findTarget = false;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Creature>()) {
                findTarget = true;
            }
        }
        if (findTarget)
        {
            controller.pointToMove.pointToMove = controller.transform.position;
        }
        else {
            controller.pointToMove.pointToMove = target;
        }
    }
}
