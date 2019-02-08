using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/AttackRangeEnough")]
public class AttackRangeEnough : Decision
{
    public override float Decide(StateController controller)
    {
        Vector3 differenceVector = controller.targetToAttack.targetToAtack.transform.position - controller.transform.position;
        float distance = (differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z);

        if (distance < controller.nextUnitAbility.rangeCast * controller.nextUnitAbility.rangeCast &&
            controller.nextUnitAbility.timeAfterLastCast > controller.nextUnitAbility.cooldown)
        {
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
