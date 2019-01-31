using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/JointAttack")]
public class JointAttack : Decision
{
    public float cooperationFactorCoefficient = 1;

    public override float Decide(StateController controller)
    {
        if (controller.targetToAttack.targetToAtack != null) {
            EnemiesAround targetEnemiesAround = controller.targetToAttack.targetToAtack.GetComponent<EnemiesAround>();
            targetEnemiesAround.FindEnemiesAround();
            float cooperationFactor = (controller.AISkills.cooperation) * cooperationFactorCoefficient;
            if (targetEnemiesAround.enemiesAround.Count * cooperationFactor > 150)
            {
                return float.MaxValue;
            }
        }
        return float.MinValue;
    }
}
