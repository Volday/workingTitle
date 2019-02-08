using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESkill : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    private void Start()
    {
        base.Start();
        rangeCast = 18;
        cooldown = 4;
    }

    public override void UseAbility(GameObject abilityTarget)
    {
        if (CooldownReady())
        {
            
            GameObject newProjectile = Instantiate(projectile, abilityTarget.transform.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.pernicious = true;
            projectileComponent.lifeTime = 1.5f;
            projectileComponent.radius = 4;

            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            timeAfterLastCast = 0;
        }
        Destroy(abilityTarget);
        CastAbilityEnd();
    }

    public override float TimeToActivate(float distance)
    {
        return 1;
    }
}
