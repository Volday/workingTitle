using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : UnitAbility
{
    public GameObject projectile;

    private TimeManager timeManager;

    AISkills aISkills;

    public float castDuration = 1;
    public float radius = 5;

    private void Start()
    {
        base.Start();

        aISkills = GetComponent<AISkills>();

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        rangeCast = 20;
        damage = 100;
        cooldown =5;
    }

    public override void UseAbility()
    {
        if (CooldownReady())
        {
            CastAbilityStart();
            GameObject newProjectile;
            if (GetComponent<StateController>() != null && GetComponent<StateController>().isActiveAndEnabled)
            {
                newProjectile = Instantiate(projectile, GetComponent<StateController>().targetToAttack.targetToAttack.GetComponent<LastStaps>().GetMotionVector(castDuration), transform.rotation);
            }
            else
            {
                newProjectile = Instantiate(projectile, FindFrontTragetToCast(), transform.rotation);
            }
            newProjectile.GetComponent<Projectile>().radius = GetComponent<RadiusAffect>().currentRadius * radius;
            newProjectile.GetComponent<Projectile>().damage = GetComponent<Damage>().currentDamage * damage;
            newProjectile.GetComponent<Projectile>().lifeTime = castDuration + 0.5f;
            newProjectile.GetComponent<UnitTeam>().name = GetComponent<UnitTeam>().name;
            timeManager.AddAction(CastAbilityEnd, castDuration + 0.002f, this);
            timeAfterLastCast = 0;
        }
    }
}
