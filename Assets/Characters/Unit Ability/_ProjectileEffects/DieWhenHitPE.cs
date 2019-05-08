using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/DieWhenHitPE")]
public class DieWhenHitPE : ProjectileEffect
{
    public override void DoProjectileEffect(GameObject carrier)
    {
        carrier.AddComponent<DieWhenHitPEComponent>();
    }
}
