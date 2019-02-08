using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/DealDamageInAreaAE")]
public class DealDamageInAreaPE : ProjectileEffect
{
    public float damage = 40;
    public float activationTime = 1;

    public override void DoProjectileEffect(GameObject carrier)
    {
        DealDamageInAreaPEComponent newMoveForwardPEComponent = carrier.AddComponent<DealDamageInAreaPEComponent>();
        newMoveForwardPEComponent.damage = damage;
        newMoveForwardPEComponent.activationTime = activationTime;
    }
}
