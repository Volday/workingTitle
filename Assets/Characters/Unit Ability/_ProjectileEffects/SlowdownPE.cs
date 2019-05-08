using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/SlowdownPE")]
public class SlowdownPE : ProjectileEffect
{
    public override void DoProjectileEffect(GameObject carrier)
    {
        SlowdownPEComponent newMoveForwardPEComponent = carrier.AddComponent<SlowdownPEComponent>();
    }
}
