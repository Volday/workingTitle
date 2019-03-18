using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    public float angle = 60;
    private float currentAngle = 60;
    public int projectileCount = 15;
    private int currentProjectileCount = 15;
    public float castDuration = 3;

    private TimeManager timeManager;

    private GameObject target;

    private void Start()
    {
        base.Start();

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        flyingProjectile = true;
        rangeCast = 10;
    }

    public override void UseAbility(GameObject abilityTarget)
    {
        if (CooldownReady())
        {
            CastAbilityStart();
            currentAngle *= GetComponent<RadiusAffect>().currentRadius * currentRadiusAffect;
            if (GetComponent<RadiusAffect>().currentRadius > 1) {
                currentProjectileCount = (int)(currentProjectileCount * GetComponent<RadiusAffect>().currentRadius * currentRadiusAffect);
            }
            for (int t = 0; t < currentProjectileCount; t++) {
                timeManager.AddAction(ThrowFlame, ((float)t / (float)currentProjectileCount) * castDuration, this);
            }
            timeManager.AddAction(CastAbilityEnd, castDuration, this);
            timeAfterLastCast = 0;
            target = abilityTarget;
        }
    }

    void ThrowFlame() {
        Debug.Log("juuj");
        //CastAbilityStart(); незабудь
    }

    public override float TimeToActivate(float distance)
    {
        return 0;
    }
}
