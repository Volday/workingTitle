using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToBeCareful")]
public class ChoosePointToBeCareful : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.GetComponent<ChoosePointToBeCarefulDone>() == null) {
            TimeManager timeManager;
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            timeManager = gameManager.GetComponent<TimeManager>();

            float distanceToTargetPosition = Mathf.Sqrt(MyMath.sqrDistanceFromPointToPoint(controller.targetToAttack.targetToAttack.transform.position, controller.transform.position));
            Vector3 targetPosition = controller.targetToAttack.targetToAttack.transform.position;
            Vector3 fromTargetToMe = controller.transform.position - targetPosition;
            float rangeCoefficient = controller.nextUnitAbility.rangeCast / distanceToTargetPosition;
            Vector3 fromTargetToFMe = Vector3.Lerp(Vector3.zero, fromTargetToMe, rangeCoefficient);
            if (rangeCoefficient > 2) {
                fromTargetToFMe *= rangeCoefficient;
            }
            Vector2 minDistanceToTarget = MyMath.Rotate(new Vector2(
                controller.GetComponent<CapsuleCollider>().radius + controller.targetToAttack.targetToAttack.GetComponent<CapsuleCollider>().radius,
                controller.GetComponent<CapsuleCollider>().radius + controller.targetToAttack.targetToAttack.GetComponent<CapsuleCollider>().radius),
                MyMath.Angle360BetweenСlockwiseVector2(new Vector2(fromTargetToFMe.x, fromTargetToFMe.y), new Vector2(0, 1), new Vector2(0, 0)));
            if (distanceToTargetPosition > controller.nextUnitAbility.rangeCast) {
                fromTargetToFMe = Vector3.Lerp(new Vector3(minDistanceToTarget.x, controller.transform.position.y, minDistanceToTarget.y),
                    fromTargetToFMe, 1 - controller.healthPoints.currentHealthPoints / controller.healthPoints.maxHealthPoints * 0.5f);
            }
            Vector2 fromTargetToFMe2d = MyMath.Rotate(new Vector2(fromTargetToFMe.x, fromTargetToFMe.z), 45 - timeManager.gameTime * controller.AISkills.caution % 90);
                fromTargetToFMe = new Vector3(fromTargetToFMe2d.x, fromTargetToFMe.y, fromTargetToFMe2d.y);
            Vector3 PointToMove = fromTargetToFMe + targetPosition;
            controller.pointToMove.pointToMove = PointToMove;
            ChoosePointToBeCarefulDone choosePointToBeCarefulDone = controller.gameObject.AddComponent<ChoosePointToBeCarefulDone>();
            choosePointToBeCarefulDone.updateTime = 2;
        }
    }
}
