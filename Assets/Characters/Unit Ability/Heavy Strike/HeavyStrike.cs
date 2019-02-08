using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    private void Start()
    {
        base.Start();
        flyingProjectile = true;
        rangeCast = 20;
    }

    public override void UseAbility(GameObject abilityTarget)
    {
        if (CooldownReady())
        {
            Vector3 target = abilityTarget.transform.position;
            target.y = transform.position.y;
            transform.LookAt(abilityTarget.transform);
            GameObject newProjectile = Instantiate(projectile, muzzle.muzzle.position, transform.rotation);
            newProjectile.transform.LookAt(target);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.pernicious = true;
            projectileComponent.lifeTime = 1f;
            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            timeAfterLastCast = 0;
        }
        Destroy(abilityTarget);
        CastAbilityEnd();
    }

    public override float TimeToActivate(float distance)
    {
        return distance / 20;
    }
}
