using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/AttractCreaturesPE")]
public class AttractCreaturesPE : ProjectileEffect
{
    public override void DoProjectileEffect(GameObject carrier)
    {
        AttractCreaturesPEComponent newAttractCreaturesPEComponent = carrier.AddComponent<AttractCreaturesPEComponent>();
    }
}
