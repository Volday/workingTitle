using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    public float blackHoleDuration = 5f;
    public float blackHoleForce = 4;
    public float radius = 7;

    private TimeManager timeManager;

    private void Start()
    {
        base.Start();

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        rangeCast = 15;
        cooldown = 8;
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

                newProjectile = Instantiate(projectile, FindMiddleOfTargets(GetComponent<StateController>().targetToAttack.targetToAttack, radius), transform.rotation);
            }
            else
            {
                newProjectile = Instantiate(projectile, FindFrontTragetToCast(), transform.rotation);
            }
            newProjectile.GetComponent<Projectile>().radius = GetComponent<RadiusAffect>().currentRadius * radius;
            newProjectile.GetComponent<UnitTeam>().name = GetComponent<UnitTeam>().name;
            newProjectile.GetComponent<Projectile>().lifeTime = blackHoleDuration + 0.5f;
            newProjectile.GetComponent<Weight>().weight = blackHoleForce;

            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);
            newProjectile.GetComponent<UnitTeam>().name = "BlackHole";

            timeManager.AddAction(CastAbilityEnd, castDuration + 0.002f, this);
        }
    }
}
