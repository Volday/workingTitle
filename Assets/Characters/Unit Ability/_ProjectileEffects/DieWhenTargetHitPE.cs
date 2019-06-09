using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/DieWhenTargetHitPE")]
public class DieWhenTargetHitPE : ProjectileEffect
{
    public override void DoProjectileEffect(GameObject carrier)
    {
        carrier.AddComponent<DieWhenTargetHitPEComponent>();
    }
}
