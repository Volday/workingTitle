using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrikeVFXDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        Transform projectileVFX = transform.Find("Magic fire 2");
        if (projectileVFX != null) {
            projectileVFX.parent = null;
            projectileVFX.GetComponent<ParticleSystem>().Stop(true);
            projectileVFX.gameObject.AddComponent<DestroyAfterTime>();
        }
    }
}
