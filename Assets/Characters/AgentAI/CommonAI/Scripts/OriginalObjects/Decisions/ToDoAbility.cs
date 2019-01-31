using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToDoAbility")]
public class ToDoAbility : Decision
{
    public override float Decide(StateController controller)
    {
        MeleeAttack meleeAttack = controller.GetComponent<MeleeAttack>();
        controller.nextUnitAbility = meleeAttack;
        Vector3 differenceVector = controller.targetToAttack.targetToAtack.transform.position - controller.transform.position;
        float distance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
        if (meleeAttack.timeAfterLastCast > meleeAttack.cooldown && distance < meleeAttack.rangeCast * meleeAttack.rangeCast)
        {
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
