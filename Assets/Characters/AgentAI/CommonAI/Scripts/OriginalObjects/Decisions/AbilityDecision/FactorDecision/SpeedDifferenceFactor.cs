using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/SpeedDifferenceFactor")]
public class SpeedDifferenceFactor : DecisionFactor
{
    public override float GetDecisionFactor(StateController controller)
    {
        float speedDifferenceFactor = 0;
        if (controller.moveSpeed.moveSpeed > 0)
        {
            float targetMoveSpeed = controller.targetToAttack.targetToAttack.GetComponent<MoveSpeed>().moveSpeed;
            float speedDifference = controller.moveSpeed.moveSpeed - targetMoveSpeed;
            float halfOfYourSpeed = controller.moveSpeed.moveSpeed / 2;
            if (speedDifference > halfOfYourSpeed)
            {
                speedDifferenceFactor = 50;
            }
            else if (speedDifference < -halfOfYourSpeed)
            {
                speedDifferenceFactor = -50;
            }
            else
            {
                speedDifferenceFactor = ((controller.moveSpeed.moveSpeed - targetMoveSpeed) / (halfOfYourSpeed) * 50);
            }
        }
        else {
            speedDifferenceFactor = -50;
        }
        return speedDifferenceFactor;
    }
}
