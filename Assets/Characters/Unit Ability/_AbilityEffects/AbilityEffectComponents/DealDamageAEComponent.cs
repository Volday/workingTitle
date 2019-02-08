using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageAEComponent : AbilityEffectsComponent
{
    public float damage;

    void Start()
    {
        HealthPoints targetHealthPoints = GetComponent<HealthPoints>();
        if (targetHealthPoints != null && GetComponent<Creature>() != null &&
            GetComponent<UnitTeam>().name != owner.GetComponent<UnitTeam>().name &&
            GetComponent<UnitTeam>().name != "Dead") {
            targetHealthPoints.TakeDamage(damage);
        }
        Destroy(this);
    }
}
