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
        //rangeCast = 20;
        projectileSpeed = 20;
        damage = 30;
    }

    public override void UseAbility()
    {
        if (CooldownReady())
        {
            GameObject newAbilityTarget = new GameObject();
            if (GetComponent<StateController>() != null && GetComponent<StateController>().isActiveAndEnabled)
            {
                //if (rangeCast > 10)
                //{
                    newAbilityTarget.transform.position = GetComponent<StateController>().futureTargetPosition;
                //}else
                //{
                    //newAbilityTarget.transform.position = GetComponent<TargetToAttack>().targetToAttack.GetComponent<LastStaps>().GetMotionVector(1);
                //}
            }
            else {
                if (GetComponent<InputManager>() != null && GetComponent<InputManager>().isActiveAndEnabled)
                {
                    newAbilityTarget.transform.position = FindFrontTragetToCast();
                }
            }
            Vector3 target = newAbilityTarget.transform.position;
            target.y = transform.position.y;
            transform.LookAt(newAbilityTarget.transform);
            GameObject newProjectile = Instantiate(projectile, muzzle.muzzle.position, transform.rotation);
            newProjectile.transform.LookAt(target);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.pernicious = true;
            projectileComponent.lifeTime = 1f;
            projectileComponent.damage = GetComponent<Damage>().currentDamage * damage;
            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            timeAfterLastCast = 0;
            Destroy(newAbilityTarget);
            CastAbilityEnd();
        }
    }
}
