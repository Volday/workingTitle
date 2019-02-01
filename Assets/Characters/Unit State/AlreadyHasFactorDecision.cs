using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlreadyHasFactorDecision : MonoBehaviour
{
    Dictionary<Decision, float> decisionFactorsValue = new Dictionary<Decision, float>();

    public float GetFactorValue(Decision _decisionFactor) {
        if (decisionFactorsValue.ContainsKey(_decisionFactor))
        {
            return decisionFactorsValue[_decisionFactor];
        }
        else {
            return float.MinValue;
        }
    }

    public void SetFactorValue(Decision _decisionFactor, float _value)
    {
        if (!decisionFactorsValue.ContainsKey(_decisionFactor))
        {
            decisionFactorsValue.Add(_decisionFactor, _value);
        }
    }
}
