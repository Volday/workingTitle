using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileEffects))]
[RequireComponent(typeof(MoveSpeed))]
[RequireComponent(typeof(HealthPoints))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(UnitTeam))]
[RequireComponent(typeof(EnemiesAround))]
public class Projectile : MonoBehaviour
{
    public float radius = 0;
    public GameObject owner;
    MoveSpeed moveSpeed;
    public bool penetrating = false;
    public float lifeTime = 5;
    TimeManager timeManager;
    EnemiesAround enemiesAround;
    public RaycastHit hit;
    HealthPoints healthPoints;

    public ProjectileEffects projectileEffects;

    private void Start()
    {
        healthPoints = GetComponent<HealthPoints>();
        enemiesAround = GetComponent<EnemiesAround>();
        projectileEffects = GetComponent<ProjectileEffects>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(GetComponent<Death>().Die, lifeTime, gameObject);
        moveSpeed = gameObject.GetComponent<MoveSpeed>();
    }

    private void Update()
    {
        if (moveSpeed.moveSpeed != 0) {
            CheckCollisions();
        }
        projectileEffects.UpdateProjectileEffect();
    }

    void CheckCollisions()
    {
        float moveDistance = moveSpeed.moveSpeed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            hit = new RaycastHit();
            if (hit.collider.gameObject.GetComponent<HealthPoints>() != null)
            {
                if (!hit.collider.gameObject.GetComponent<Collider>().isTrigger && hit.collider.gameObject.GetComponent<Creature>() != null)
                {
                    AbilityEffects targetAbilityEffects = hit.collider.gameObject.GetComponent<AbilityEffects>();
                    if (targetAbilityEffects != null)
                    {
                        for (int t = 0; t < projectileEffects.abilityEffects.Count; t++)
                        {
                            targetAbilityEffects.abilityEffects.Add(projectileEffects.abilityEffects[t]);
                        }
                    }
                    if (!penetrating) {
                        GetComponent<Death>().Die();
                    }
                }
                else
                {
                    Projectile targetProjectile = hit.collider.gameObject.GetComponent<Projectile>();
                    if (hit.collider.gameObject.GetComponent<Collider>().isTrigger && hit.collider.gameObject.GetComponent<Projectile>() != null)
                    {
                        if (!targetProjectile.penetrating && !penetrating) {
                        }else if (targetProjectile.penetrating && !penetrating) {
                            GetComponent<Death>().Die();
                        }else if (penetrating) {
                            HealthPoints targetHealthPoints = targetProjectile.gameObject.GetComponent<HealthPoints>();
                            float exchangeOfHealth = targetHealthPoints.currentHealthPoints;
                            targetHealthPoints.currentHealthPoints -= healthPoints.currentHealthPoints;
                            healthPoints.currentHealthPoints -= exchangeOfHealth;
                        }
                    }
                }
            }
        }
    }
}
