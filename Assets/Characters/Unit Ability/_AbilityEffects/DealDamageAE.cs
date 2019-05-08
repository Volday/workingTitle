using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityEffect/DealDamageAE")]
public class DealDamageAE : AbilityEffect
{
    public override void DoAbilityEffect(GameObject target, GameObject owner, GameObject projectile)
    {
        DealDamageAEComponent dealDamageAEComponent = target.AddComponent<DealDamageAEComponent>();
        dealDamageAEComponent.damage = projectile.GetComponent<Damage>().currentDamage * projectile.GetComponent<Projectile>().damage;
    }
}
