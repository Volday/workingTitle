using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAbility : MonoBehaviour
{
    public float cooldown = 0;
    [HideInInspector] public float timeAfterLastCast = 0;
    public float rangeCast = 0;
    public float value = 1;

    public Decision decision;
    public bool flyingProjectile = false;
    [HideInInspector] public Muzzle muzzle;

    public abstract void UseAbility(GameObject abilityTarget);

    public abstract float TimeToActivate(float distance);

    public virtual void Start()
    {
        muzzle = GetComponent<Muzzle>();
        UnitAbilities unitAbilities = GetComponent<UnitAbilities>();
        if (unitAbilities != null) {
            unitAbilities.unitAbilities.Add(this);
        }
    }

    private void Update()
    {
        timeAfterLastCast += Time.deltaTime;
    }

    public void CastAbilityEnd() {
        CastAbilityTime castAbilityTime = gameObject.GetComponent<CastAbilityTime>();
        if (castAbilityTime != null) {
            Destroy(castAbilityTime);
        }
    }

    public bool CooldownReady()
    {
        if (timeAfterLastCast > cooldown)
        {
            return true;
        }
        return false;
    }

    public void SetEffectsToProjectile(GameObject projectile, List<AbilityEffect> abilityEffects, List<ProjectileEffect> projectileEffects) {
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.owner = gameObject;

        UnitTeam newProjectileUnitTeam = projectile.GetComponent<UnitTeam>();
        newProjectileUnitTeam.name = GetComponent<UnitTeam>().name;

        ProjectileEffectsManager projectileEffectsManager = projectile.GetComponent<ProjectileEffectsManager>();
        if (projectileEffectsManager != null)
        {
            for (int t = 0; t < abilityEffects.Count; t++)
            {
                projectileEffectsManager.abilityEffects.Add(abilityEffects[t]);
            }
            for (int t = 0; t < projectileEffects.Count; t++)
            {
                projectileEffectsManager.projectileEffects.Add(projectileEffects[t]);
            }
        }
    }
}
