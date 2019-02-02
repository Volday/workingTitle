using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToDoAbility")]
public class ToDoAbility : Decision
{
    public override float Decide(StateController controller)
    {
        Vector3 differenceVector = controller.targetToAttack.targetToAtack.transform.position - controller.transform.position;
        float distance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
        if (controller.nextUnitAbility.timeAfterLastCast > controller.nextUnitAbility.cooldown && distance 
            < controller.nextUnitAbility.rangeCast * controller.nextUnitAbility.rangeCast)
        {
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
