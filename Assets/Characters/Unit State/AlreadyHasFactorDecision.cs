using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlreadyHasFactorDecision : MonoBehaviour
{
    Dictionary<DecisionFactor, float> decisionFactorsValue = new Dictionary<DecisionFactor, float>();

    public float GetFactorValue(DecisionFactor _decisionFactor) {
        if (decisionFactorsValue.ContainsKey(_decisionFactor))
        {
            return decisionFactorsValue[_decisionFactor];
        }
        else {
            return float.MinValue + 1;
        }
    }

    public void SetFactorValue(DecisionFactor _decisionFactor, float _value)
    {
        if (!decisionFactorsValue.ContainsKey(_decisionFactor))
        {
            decisionFactorsValue.Add(_decisionFactor, _value);
        }
    }
}
