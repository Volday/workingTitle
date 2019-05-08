using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageAEComponent : AbilityEffectsComponent
{
    public float damage;

    void Start()
    {
        HealthPoints healthPoints = GetComponent<HealthPoints>();
        if (healthPoints != null)
        {
            healthPoints.TakeDamage(damage);
        }
        Destroy(this);
    }
}
