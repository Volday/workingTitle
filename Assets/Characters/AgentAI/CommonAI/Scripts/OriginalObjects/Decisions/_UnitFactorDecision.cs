using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/_UnitFactorDecision")]
public class _UnitFactorDecision : Decision
{
    public Factor[] factors;

    public override float Decide(StateController controller)
    {
        float factorsSum = 0;
        for (int t = 0; t < factors.Length; t++) {
            factorsSum += factors[t].decisionFactors.GetDecisionFactor() * factors[t].factorCoefficient;
        }
        return factorsSum;
    }

    [System.Serializable]
    public class Factor {
        public DecisionFactor decisionFactors;
        public float factorCoefficient = 1;
    }
}
