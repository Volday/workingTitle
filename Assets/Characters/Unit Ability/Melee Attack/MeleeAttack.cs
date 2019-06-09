using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    public float radius = 1;

    private void Start()
    {
        base.Start();
        rangeCast = 3;
        cooldown = 2;
        damage = 20;
    }

    //Здесь что-то не так не используй, лучше перепиши
    public override void UseAbility()
    {
        if (CooldownReady())
        {
            CastAbilityStart();
            GetComponent<TargetToAttack>().targetToAttack.GetComponent<HealthPoints>().TakeDamage(20);
            //Vector3 target = GetComponent<TargetToAttack>().targetToAttack.transform.position;
            //target.y = transform.position.y;
            //GameObject newProjectile = Instantiate(projectile, target, transform.rotation);
            //newProjectile.GetComponent<Projectile>().damage = GetComponent<Damage>().currentDamage * damage;
            //newProjectile.GetComponent<Projectile>().radius = GetComponent<RadiusAffect>().currentRadius * radius;
            //newProjectile.GetComponent<Projectile>().lifeTime = 0;
            //SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);
            //timeAfterLastCast = 0;
            CastAbilityEnd();
        }
    }
}
