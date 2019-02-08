using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityEffect/DealDamageAE")]
public class DealDamageAE : AbilityEffect
{
    public float damage;

    public override void DoAbilityEffect(GameObject target, GameObject owner)
    {
        DealDamageAEComponent newDealDamageAEComponent = target.AddComponent<DealDamageAEComponent>();
        newDealDamageAEComponent.damage = damage;
        newDealDamageAEComponent.owner = owner;
    }
}
