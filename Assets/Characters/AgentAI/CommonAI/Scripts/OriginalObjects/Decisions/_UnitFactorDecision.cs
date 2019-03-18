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
            float nextFactor = factors[t].decisionFactors.GetDecisionFactor(controller);
            if (nextFactor == float.MinValue) {
                return float.MinValue;
            }
            factorsSum += (nextFactor + factors[t].factorShift * 50) * factors[t].factorCoefficient;
            factorCoefficientSum += Mathf.Abs(factors[t].factorCoefficient);
        }
        float factorsCount = factors.Count;
        //factorsSum = factorsSum * (factorsCount / factorCoefficientSum); нормализовал коэфициенты, чтобы их средний коэфициенты был равен 1
        //factorsSum = factorsSum * (4 / factorsCount); нормализовал факторы, как-будто их всегда 4 штуки
        return factorsSum;
    }

    [System.Serializable]
    public class Factor {
        public DecisionFactor decisionFactors;
        public float factorCoefficient = 1;
        [Range(-1,1)]
        public float factorShift;
        public Factor() {
            factorCoefficient = 1;
        }
    }
}
