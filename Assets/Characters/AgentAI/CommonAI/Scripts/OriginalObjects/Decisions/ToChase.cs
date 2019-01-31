using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToChase")]
public class ToChase : Decision
{
    public float balanceCoefficient = 1;
    public float speedFactorCoefficient = 1;
    public float agilityFactorCoefficient = 1;
    public float aggressionFactorCoefficient = 1;

    public override float Decide(StateController controller)
    {
        float speedFactor = (controller.moveSpeed.moveSpeed -
            controller.targetToAttack.targetToAtack.GetComponent<MoveSpeed>().moveSpeed) * speedFactorCoefficient;
        float aggressionFactor = (controller.AISkills.aggression - 50) * aggressionFactorCoefficient;
        float agilityFactor = (controller.AISkills.agility - 50) * agilityFactorCoefficient;
        return (speedFactor + agilityFactor + aggressionFactor) * balanceCoefficient;
    }
}
