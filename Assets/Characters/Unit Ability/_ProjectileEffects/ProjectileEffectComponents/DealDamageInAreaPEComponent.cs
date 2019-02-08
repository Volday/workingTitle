using System.Collections.Generic;
using UnityEngine;

public class DealDamageInAreaPEComponent : MonoBehaviour
{
    public float damage = 0;
    public float activationTime = 0;

    void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeManager>().AddAction(DealDamageInAreaPEInTime, activationTime, this);
    }

    void DealDamageInAreaPEInTime()
    {
        List<GameObject> targets = GetComponent<EnemiesAround>().FindTargetsAroundInRadius(GetComponent<Projectile>().radius);
        for (int t = 0; t < targets.Count; t++)
        {
            if (targets[t].GetComponent<Creature>() != null &&
                targets[t].GetComponent<UnitTeam>().name != GetComponent<UnitTeam>().name &&
                targets[t].GetComponent<UnitTeam>().name != "Dead")
            {
                HealthPoints targetHealthPoint = targets[t].GetComponent<HealthPoints>();
                if (targetHealthPoint != null)
                {
                    targetHealthPoint.TakeDamage(damage);
                }
            }
        }

        Destroy(this);
    }
}
