using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilities : MonoBehaviour
{
    public List<UnitAbility> unitAbilities;

    private void Update()
    {
        if (GetComponent<CastAbilityTime>() == null) {
            AbilityComponent[] abilityComponents = GetComponents<AbilityComponent>();
            for (int t = 0; t < abilityComponents.Length; t++) {
                abilityComponents[t].CastEnd();
            }
        }
    }
}
