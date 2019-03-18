using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecisionFactor/NumberOfAlliesFactor")]
public class NumberOfAlliesFactor : DecisionFactor
{
    public float midNumberOfAllies = 2;
    public override float GetDecisionFactor(StateController controller)
    {
        float numberOfAlliesFactorValue = ((controller.targetToAttack.targetToAttack.GetComponent<EnemiesAround>().FindEnemiesAround() - 1)
            / (2 * midNumberOfAllies) - 0.5f) * 100;
        if (numberOfAlliesFactorValue > 50) {
            numberOfAlliesFactorValue = 50;
        }
        return numberOfAlliesFactorValue;
    }
}
