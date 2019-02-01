using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : UnitAbility
{
    public GameObject projectile;

    private void Start()
    {
        base.Start();
        flyingProjectile = true;
    }

    public override void UseAbility(GameObject abilityTarget)
    {
        if (timeAfterLastCast > cooldown)
        {
            Instantiate(projectile, muzzle.muzzle.position, transform.rotation);
            timeAfterLastCast = 0;
        }
    }
}
