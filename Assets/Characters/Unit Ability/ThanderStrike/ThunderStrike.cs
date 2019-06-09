using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    private TimeManager timeManager;

    AISkills aISkills;

    public float applicationDelay = 1;
    public float radius = 5;

    private void Start()
    {
        base.Start();

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        rangeCast = 20;
        damage = 100;
        cooldown =5;
        castDuration = 0.4f;
    }

    public override void UseAbility()
    {
        if (CooldownReady())
        {
            CastAbilityStart();
            GameObject newProjectile;
            if (GetComponent<StateController>() != null && GetComponent<StateController>().isActiveAndEnabled)
            {
                newProjectile = Instantiate(projectile, GetComponent<StateController>().targetToAttack.targetToAttack.GetComponent<LastStaps>().GetMotionVector(applicationDelay), transform.rotation);
            }
            else
            {
                newProjectile = Instantiate(projectile, FindFrontTragetToCast(), transform.rotation);
            }
            newProjectile.GetComponent<Projectile>().radius = GetComponent<RadiusAffect>().currentRadius * radius;
            newProjectile.GetComponent<Projectile>().damage = GetComponent<Damage>().currentDamage * damage;
            newProjectile.GetComponent<Projectile>().lifeTime = applicationDelay + 0.5f;

            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            timeManager.AddAction(CastAbilityEnd, applicationDelay + 0.002f, this);
        }
    }
}
