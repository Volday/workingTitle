using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/DealDamageInSoloTargetPE")]
public class DealDamageInSoloTargetPE : ProjectileEffect
{
    public float damage = 20;

    public override void DoProjectileEffect(GameObject carrier)
    {
        DealDamageInSoloTargetPEComponent newDealDamageInSoloTargetPEComponent = carrier.AddComponent<DealDamageInSoloTargetPEComponent>();
        newDealDamageInSoloTargetPEComponent.damage = damage;
    }
}
