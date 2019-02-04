using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffects : MonoBehaviour
{
    public List<ProjectileEffect> projectileEffects;
    public List<AbilityEffect> abilityEffects;

    public void UpdateProjectileEffect()
    {
        for (int t = 0; t < projectileEffects.Count; t++)
        {
            projectileEffects[t].DoProjectileEffect();
        }
    }
}
