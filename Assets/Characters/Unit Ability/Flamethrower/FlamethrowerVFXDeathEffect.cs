using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerVFXDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        Transform projectileVFX = transform.Find("Flamethrower_FireBall");
        projectileVFX.parent = null;
        projectileVFX.GetComponent<ParticleSystem>().Stop(true);
        projectileVFX.gameObject.AddComponent<DestroyAfterTime>();
    }
}
