using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAbilityEffectsInRadiusAffectPEComponent : MonoBehaviour
{
    Projectile projectile;
    EnemiesAround enemiesAround;
    RadiusAffect radiusAffect;
    ProjectileEffectsManager projectileEffectsManager;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
        enemiesAround = GetComponent<EnemiesAround>();
        radiusAffect = GetComponent<RadiusAffect>();
        projectileEffectsManager = GetComponent<ProjectileEffectsManager>();
    }

    private void Update()
    {
        ApplyWithRadiusAffect();
        ApplyWithHit();
    }

    void ApplyWithRadiusAffect() {
        List<GameObject> targetsForApply = enemiesAround.FindEnemiesAround(radiusAffect.currentRadius);
        if (targetsForApply.Count > 0) {
            for (int t = 0; t < targetsForApply.Count; t++) {
                projectileEffectsManager.ApplyAbilityEffects(targetsForApply[t]);
                AddTargetHitPEComponent();
            }
        }
    }

    void ApplyWithHit() {
        if (projectile.haveHit == true) {
            if (projectile.hit.collider.gameObject.GetComponent<Creature>() != null) {
                if (projectile.hit.collider.gameObject.GetComponent<UnitTeam>().team != GetComponent<UnitTeam>().team) {
                    projectileEffectsManager.ApplyAbilityEffects(projectile.hit.collider.gameObject);
                    AddTargetHitPEComponent();
                }
            }
        }
    }

    void AddTargetHitPEComponent() {
        if (gameObject.GetComponent<TargetHitPE>() != null) {
            gameObject.AddComponent<TargetHitPE>();
        }
    }
}
