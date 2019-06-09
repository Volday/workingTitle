using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AISkills))]

public abstract class UnitAbility : MonoBehaviour
{
    public float cooldown = 0;
    [HideInInspector] public float timeAfterLastCast = 0;
    public float rangeCast = 0;
    public float value = 1;
    public float projectileSpeed = 0;
    public float castDuration = 0;

    public Decision decision;
    public bool flyingProjectile = false;
    public bool pernicious = false;
    [HideInInspector] public Muzzle muzzle;
    public float damage;

    public abstract void UseAbility();

    public float TimeToActivate(float distance)
    {
        if (projectileSpeed != 0)
        {
            return distance / projectileSpeed;
        }
        else {
            return 0;
        }
    }

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

    public void CastAbilityStart()
    {
        timeAfterLastCast = 0;
        if (gameObject.GetComponent<CastAbilityTime>() == null)
        {
            gameObject.AddComponent<CastAbilityTime>();
        }
    }

    public Vector3 FindMiddleOfTargets(GameObject target, float radius) {
        List<GameObject> targets = FindGroupOfTargets(target, radius);
        Vector3 middleOfTargets = Vector3.zero;
        for (int t = 0; t < targets.Count; t++) {
            middleOfTargets += targets[t].transform.position;
        }
        return middleOfTargets / (float)targets.Count;
    }

    public List<GameObject> FindGroupOfTargets(GameObject target, float radius)
    {
        List<GameObject> enemiesAroundMe = GetComponent<EnemiesAround>().FindEnemiesAround(rangeCast);
        List<GameObject> enemiesAroundTarget = target.GetComponent<EnemiesAround>().FindTargetsCreatureAroundInRadius(rangeCast);
        List<GameObject> groupOfTargets = new List<GameObject>();
        for (int t = 0; t < enemiesAroundMe.Count; t++) {
            for (int i = 0; i < enemiesAroundTarget.Count; i++)
            {
                if (enemiesAroundMe[t] == enemiesAroundTarget[i]) {
                    groupOfTargets.Add(enemiesAroundMe[t]);
                }
            }
        }
        return groupOfTargets;
    }

    public Vector3 FindFrontTragetToCast() {
        Vector3 tragetToCast;
        Camera mainCamera;
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Landscape")))
        {
            tragetToCast = hit.point;
        }
        else {
            tragetToCast = Vector3.zero;
        }
        tragetToCast.y = muzzle.transform.position.y;
        return tragetToCast;
    }

    public bool CooldownReady()
    {
        if (timeAfterLastCast > cooldown && gameObject.GetComponent<CastAbilityTime>() == null)
        {
            return true;
        }
        return false;
    }

    public void SetEffectsToProjectile(GameObject projectile, List<AbilityEffect> abilityEffects, List<ProjectileEffect> projectileEffects) {

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.owner = gameObject;
        projectileComponent.GetComponent<MoveSpeed>().baseMoveSpeed = projectileSpeed;
        projectileComponent.GetComponent<RadiusAffect>().baseRadius = GetComponent<RadiusAffect>().currentRadius;

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
