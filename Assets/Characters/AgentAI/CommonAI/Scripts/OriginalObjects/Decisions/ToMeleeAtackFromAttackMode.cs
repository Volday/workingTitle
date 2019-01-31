using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToMeleeAtackFromAttackMode")]
public class ToMeleeAtackFromAttackMode : Decision
{
    public float balanceCoefficient = 1;
    public float speedFactorCoefficient = 1;
    public float damageFactorCoefficient = 1;
    public float healthPointsFactorCoefficient = 1;
    public float cooldawnFactorCoefficient = 1;
    public float agilityFactorCoefficient = 1;

    public override float Decide(StateController controller)
    {
        MeleeAttack meleeAttack = controller.GetComponent<MeleeAttack>();
        if (meleeAttack != null)
        {
            float speedFactor = (controller.moveSpeed.moveSpeed -
                controller.targetToAttack.targetToAtack.GetComponent<MoveSpeed>().moveSpeed) * speedFactorCoefficient;
            float damageFactor = meleeAttack.Damage * damageFactorCoefficient;
            float healthPointsFactor = controller.healthPoints.currentHealthPoints * healthPointsFactorCoefficient;
            float cooldawnFactor = 0;
            if (meleeAttack.timeAfterLastCast > meleeAttack.cooldown)
            {
                cooldawnFactor = 100 * cooldawnFactorCoefficient;
            }
            float agilityFactor = (controller.AISkills.agility - 50) * agilityFactorCoefficient;
            return (speedFactor + damageFactor + healthPointsFactor + cooldawnFactor + agilityFactor) * balanceCoefficient;
        }
        else {
            return float.MinValue;
        }
    }
}
