using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/ThunderStrikePE")]
public class ThunderStrikePE : ProjectileEffect
{
    public GameObject vfxProjectile;

    public override void DoProjectileEffect(GameObject carrier)
    {
        ThunderStrikePEComponent newMoveForwardPEComponent = carrier.AddComponent<ThunderStrikePEComponent>();
        newMoveForwardPEComponent.vfxProjectile = vfxProjectile;
    }
}
