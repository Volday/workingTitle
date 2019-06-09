using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/ApplyAbilityEffectsInRadiusAffectPE")]
public class ApplyAbilityEffectsInRadiusAffectPE : ProjectileEffect
{
    public override void DoProjectileEffect(GameObject carrier)
    {
        carrier.AddComponent<ApplyAbilityEffectsInRadiusAffectPEComponent>();
    }
}
