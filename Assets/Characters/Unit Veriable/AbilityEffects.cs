using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEffects : MonoBehaviour
{
    public List<AbilityEffect> abilityEffects;

    void Update()
    {
        for (int t = 0; t < abilityEffects.Count; t++) {
            abilityEffects[t].DoAbilityEffect();
        }
    }
}
