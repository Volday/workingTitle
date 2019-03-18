using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/EnemyClose")]
public class EnemyClose : Decision
{
    public override float Decide(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack != null)
        {
            Vector3 differenceVector = controller.targetToAttack.targetToAttack.transform.position - controller.transform.position;
            float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
            if (newDistance < 36) {
                return float.MaxValue;
            }
        }
        return float.MinValue;
    }
}
