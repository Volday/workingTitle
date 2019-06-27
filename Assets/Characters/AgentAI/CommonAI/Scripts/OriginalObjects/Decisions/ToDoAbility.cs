using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToDoAbility")]
public class ToDoAbility : Decision
{
    public override float Decide(StateController controller)
    {
        if (controller.nextUnitAbility != null)
        {
            Vector3 differenceVector;
            Vector3 futureTargetPosition;
            float distance;
            float accuracyCoefficient = controller.AISkills.accuracy / 100;
            Vector3 muzzle = controller.GetComponent<Muzzle>().muzzle.position;
            if (controller.nextUnitAbility.flyingProjectile)
            {
                //растояние до цели
                differenceVector = controller.targetToAttack.targetToAttack.transform.position - muzzle;
                distance = Mathf.Sqrt((differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z));

                //первичная будующая позиция
                futureTargetPosition = controller.targetToAttack.targetToAttack.GetComponent<LastStaps>().GetMotionVector(
                    controller.nextUnitAbility.TimeToActivate(distance) + 0.1f);

                //растояние до будующей позиции
                differenceVector = futureTargetPosition - muzzle;
                float distanceToFutureTargetPosition = Mathf.Sqrt((differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z));

                //будующая позиция
                float distanceShift = distanceToFutureTargetPosition / distance;
                futureTargetPosition = controller.targetToAttack.targetToAttack.GetComponent<LastStaps>().GetMotionVector(
                    controller.nextUnitAbility.TimeToActivate(distanceToFutureTargetPosition * distanceShift) * accuracyCoefficient + 0.1f);
                controller.futureTargetPosition = futureTargetPosition;

                //проверка на препядствея
                Ray ray = new Ray(muzzle, futureTargetPosition - muzzle);

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, distanceToFutureTargetPosition * distanceShift))
                {
                    if (hit.collider.gameObject != controller.targetToAttack.targetToAttack) {
                        return float.MinValue;
                    }
                }
            }
            else {
                futureTargetPosition = controller.targetToAttack.targetToAttack.GetComponent<LastStaps>().GetMotionVector(
                    controller.nextUnitAbility.TimeToActivate(0) * accuracyCoefficient);
                controller.futureTargetPosition = futureTargetPosition;
            }

            differenceVector = futureTargetPosition - controller.transform.position;
            distance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
            if (controller.nextUnitAbility.timeAfterLastCast > controller.nextUnitAbility.cooldown && distance 
                < controller.nextUnitAbility.rangeCast * controller.nextUnitAbility.rangeCast)
            {
                return float.MaxValue;
            }
            else {
                return float.MinValue;
            }
        }

        return float.MinValue;
    }
}
