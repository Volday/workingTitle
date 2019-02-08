using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/ProjectileEffect/MoveForwardPE")]
public class MoveForwardPE : ProjectileEffect
{
    public float speed;

    public override void DoProjectileEffect(GameObject carrier)
    {
        MoveForwardPEComponent newMoveForwardPEComponent = carrier.AddComponent<MoveForwardPEComponent>();
        newMoveForwardPEComponent.speed = speed;
    }
}
