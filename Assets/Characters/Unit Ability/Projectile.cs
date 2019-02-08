using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileEffectsManager))]
[RequireComponent(typeof(MoveSpeed))]
[RequireComponent(typeof(HealthPoints))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(UnitTeam))]
[RequireComponent(typeof(EnemiesAround))]
[RequireComponent(typeof(ProjectileDeathEffect))]
public class Projectile : MonoBehaviour
{
    public float radius = 0;
    public GameObject owner;
    MoveSpeed moveSpeed;
    public bool penetrating = false;
    public float lifeTime = 1;
    public float spawnTime;
    TimeManager timeManager;
    EnemiesAround enemiesAround;
    public RaycastHit hit;
    HealthPoints healthPoints;
    public bool pernicious = false;
    UnitTeam myTeam;

    List<GameObject> damagedTargets = new List<GameObject>();

    ProjectileEffectsManager projectileEffectsManager;

    private void Start()
    {
        myTeam = GetComponent<UnitTeam>();
        healthPoints = GetComponent<HealthPoints>();
        healthPoints.currentHealthPoints = 1;
        healthPoints.maxHealthPoints = 1;
        enemiesAround = GetComponent<EnemiesAround>();
        projectileEffectsManager = GetComponent<ProjectileEffectsManager>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        spawnTime = timeManager.gameTime;
        timeManager.AddAction(GetComponent<Death>().Die, lifeTime, this);
        moveSpeed = gameObject.GetComponent<MoveSpeed>();
    }

    private void Update()
    {
        if (moveSpeed.moveSpeed != 0) {
            CheckCollisions();
        }
    }

    void CheckCollisions()
    {
        float moveDistance = 2 * moveSpeed.moveSpeed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);

        hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            HealthPoints healthPoints = hit.collider.gameObject.GetComponent<HealthPoints>();
            if (healthPoints != null)
            {
                if (damagedTargets.IndexOf(hit.collider.gameObject) == -1)
                {
                    damagedTargets.Add(hit.collider.gameObject);
                    Creature targetCreature = hit.collider.gameObject.GetComponent<Creature>();
                    Projectile targetProjectile = hit.collider.gameObject.GetComponent<Projectile>();

                    if (targetCreature == null && targetProjectile == null) {
                        GetComponent<Death>().Die();
                    }

                    if (!hit.collider.gameObject.GetComponent<Collider>().isTrigger && targetCreature != null && hit.collider.gameObject.GetComponent<UnitTeam>().name != myTeam.name)
                    {
                        projectileEffectsManager.ApplyAbilityEffects(hit.collider.gameObject, owner);
                        if (!penetrating)
                        {
                            GetComponent<Death>().Die();
                        }
                    }
                    else
                    {
                        if (hit.collider.gameObject.GetComponent<Collider>().isTrigger && targetProjectile != null)
                        {
                            if (!targetProjectile.penetrating && !penetrating)
                            {
                            }
                            else if (targetProjectile.penetrating && !penetrating)
                            {
                                GetComponent<Death>().Die();
                            }
                            else if (penetrating)
                            {
                                HealthPoints targetHealthPoints = targetProjectile.gameObject.GetComponent<HealthPoints>();
                                float exchangeOfHealth = targetHealthPoints.currentHealthPoints;
                                targetHealthPoints.TakeDamage(healthPoints.currentHealthPoints);
                                healthPoints.TakeDamage(exchangeOfHealth);
                            }
                        }
                    }
                }
            }
            else {
                GetComponent<Death>().Die();
            }
        }
    }
}
