using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectsManager : MonoBehaviour
{
    public List<ProjectileEffect> projectileEffects;
    public List<AbilityEffect> abilityEffects;

    void Update()
    {
        for (int t = 0; t < projectileEffects.Count; t++)
        {
            projectileEffects[t].DoProjectileEffect(gameObject);
            projectileEffects.Remove(projectileEffects[t]);
        }
    }

    public void ApplyAbilityEffects(GameObject target) {
        for (int t = 0; t < abilityEffects.Count; t++) {
            
            abilityEffects[t].DoAbilityEffect(target, gameObject.GetComponent<Projectile>().owner, gameObject);
        }
    }
}
