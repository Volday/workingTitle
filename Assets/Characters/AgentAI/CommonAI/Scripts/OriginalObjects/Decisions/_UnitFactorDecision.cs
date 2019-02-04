using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/_UnitFactorDecision")]
public class _UnitFactorDecision : Decision
{
    public List<Factor> factors;

    public override float Decide(StateController controller)
    {
        float factorsSum = 0;
        float factorCoefficientSum = 0;
        for (int t = 0; t < factors.Count; t++) {
            factorsSum += factors[t].decisionFactors.GetDecisionFactor(controller) * factors[t].factorCoefficient;
            factorCoefficientSum += Mathf.Abs(factors[t].factorCoefficient);
        }
        float factorsCount = factors.Count;
        factorsSum = factorsSum * (factorsCount / factorCoefficientSum);
        factorsSum = factorsSum * (4 / factorsCount);
        return factorsSum;
    }

    [System.Serializable]
    public class Factor {
        public DecisionFactor decisionFactors;
        public float factorCoefficient = 1;
        public Factor() {
            factorCoefficient = 1;
        }
    }
}
