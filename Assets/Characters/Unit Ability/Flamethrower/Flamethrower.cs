using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : UnitAbility
{
    public GameObject projectile;

    public List<AbilityEffect> abilityEffects;
    public List<ProjectileEffect> projectileEffects;

    public float angle = 45;
    private float currentAngle = 45;
    public int projectileCount = 30;
    private int currentProjectileCount = 30;

    private TimeManager timeManager;

    AISkills aISkills;

    private void Start()
    {
        base.Start();
        
        aISkills = GetComponent<AISkills>();

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        flyingProjectile = true;
        rangeCast = 10;
        projectileSpeed = 10;
        damage = 5;
        cooldown = 6;
        castDuration = 3;
    }

    public override void UseAbility()
    {
        if (CooldownReady())
        {
            CastAbilityStart();
            FlamethrowerComponent flamethrowerComponent = gameObject.AddComponent<FlamethrowerComponent>();
            currentAngle = angle;
            currentProjectileCount = projectileCount;
            
            currentAngle *= GetComponent<RadiusAffect>().currentRadius;
            currentProjectileCount = (int)(currentProjectileCount * (currentAngle/angle));
            for (int t = 0; t < currentProjectileCount; t++) {
                timeManager.AddAction(ThrowFlame, ((float)t / (float)currentProjectileCount) * castDuration, this);
            }
            timeManager.AddAction(CastAbilityEnd, castDuration + 0.002f, this);
        }
    }

    void ThrowFlame() {
        if (gameObject.activeSelf) {
            GameObject newAbilityTarget = new GameObject();
            if (GetComponent<StateController>() != null && GetComponent<StateController>().isActiveAndEnabled)
            {
                TargetToAttack targetToAttack = GetComponent<TargetToAttack>();
                if (targetToAttack.targetToAttack == null || targetToAttack.targetToAttack.activeSelf == false) {
                    List<GameObject> newTargets = GetComponent<EnemiesAround>().FindClosestEnemies(1);
                    if (newTargets[0] != null) {
                        targetToAttack.targetToAttack = newTargets[0];
                    }
                }
                newAbilityTarget.transform.position = targetToAttack.targetToAttack.transform.position;
            }
            else {
                if (GetComponent<InputManager>() != null && GetComponent<InputManager>().isActiveAndEnabled) {
                    newAbilityTarget.transform.position = FindFrontTragetToCast();
                }
            }
            GameObject newProjectile = Instantiate(projectile, muzzle.muzzle.position, transform.rotation);

            newProjectile.transform.LookAt(newAbilityTarget.transform.position);
            newProjectile.GetComponent<Projectile>().damage = GetComponent<Damage>().currentDamage * damage;
            newProjectile.transform.Rotate(new Vector3(aISkills.GetRandomNumber(-10, 10), aISkills.GetRandomNumber(-(int)currentAngle / 2, (int)currentAngle / 2), 0));
            newProjectile.GetComponent<Projectile>().lifeTime = 1.5f;
            newProjectile.GetComponent<UnitTeam>().name = GetComponent<UnitTeam>().name;
            newProjectile.GetComponent<RadiusAffect>().currentRadius = 0;

            SetEffectsToProjectile(newProjectile, abilityEffects, projectileEffects);

            Destroy(newAbilityTarget);
        }
    }
}
