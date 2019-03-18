using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/RunAway")]
public class RunAway : Decision
{
    public float balanceCoefficient = 1;
    public float speedFactorCoefficient = 1;
    public float aggressionFactorCoefficient = 1;
    public float healthPointsFactorCoefficient = 1;
    public float distanceFactorFactorCoefficient = 1;
    public float cooperationFactorCoefficient = 1;

    public override float Decide(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack != null)
        {
            EnemiesAround targetEnemiesAround = controller.targetToAttack.targetToAttack.GetComponent<EnemiesAround>();
            targetEnemiesAround.FindEnemiesAround();
            float cooperationFactor = (controller.AISkills.cooperation) * cooperationFactorCoefficient;
            if (targetEnemiesAround.enemiesAround.Count * cooperationFactor < 150)
            {
                float speedFactor = 0;
                if (controller.targetToAttack.targetToAttack != null)
                {
                    speedFactor = (controller.moveSpeed.moveSpeed -
                        controller.targetToAttack.targetToAttack.GetComponent<MoveSpeed>().moveSpeed) * speedFactorCoefficient;
                }
                float aggressionFactor = (controller.AISkills.aggression - 50) * aggressionFactorCoefficient;
                float healthPointsFactor = (100 - (controller.healthPoints.currentHealthPoints / controller.healthPoints.maxHealthPoints) * 100) * healthPointsFactorCoefficient;

                float distanceFactor = 0;
                if (controller.targetToAttack.targetToAttack != null && healthPointsFactor > 80)
                {
                    Vector3 differenceVector = controller.targetToAttack.targetToAttack.transform.position - controller.transform.position;
                    float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                    distanceFactor = Mathf.Clamp((newDistance - 36) * distanceFactorFactorCoefficient, -36, 36);
                }
                return (speedFactor - aggressionFactor + healthPointsFactor + distanceFactor) * balanceCoefficient;
            }
        }
        return float.MinValue;
    }
}
