using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileEffectsManager))]
[RequireComponent(typeof(MoveSpeed))]
[RequireComponent(typeof(RadiusAffect))]
[RequireComponent(typeof(HealthPoints))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(UnitTeam))]
[RequireComponent(typeof(EnemiesAround))]
[RequireComponent(typeof(ProjectileDeathEffect))]
[RequireComponent(typeof(Weight))]
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
    public float damage = 0;
    [HideInInspector]public bool haveHit = false;

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
            haveHit = true;
        }
        else {
            haveHit = false;
        }
    }
}
