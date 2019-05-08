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
        projectileSpeed = 20;
    }

    public override void UseAbility()
    {
        if (CooldownReady())
        {
            GameObject newAbilityTarget = new GameObject();
            newAbilityTarget.transform.position = FindFrontTragetToCast();
            if (GetComponent<StateController>() != null && GetComponent<StateController>().isActiveAndEnabled) {
                newAbilityTarget.transform.position = GetComponent<StateController>().futureTargetPosition;
            }
            Vector3 target = newAbilityTarget.transform.position;
            target.y = transform.position.y;
            transform.LookAt(newAbilityTarget.transform);
            GameObject newProjectile = Instantiate(projectile, muzzle.muzzle.position, transform.rotation);
            newProjectile.transform.LookAt(target);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.pernicious = true;
            projectileComponent.lifeTime = 1f;
            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            timeAfterLastCast = 0;
            Destroy(newAbilityTarget);
        }
        CastAbilityEnd();
    }
}
