using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamageInSoloTargetPEComponent : MonoBehaviour
{
    public float damage = 1;

    void Start()
    {
        EnemiesAround enemiesAround = GetComponent<EnemiesAround>();
        List<GameObject> target = enemiesAround.FindClosestEnemies(1);

        if (target.Count > 0 && target[0] != null) {
            target[0].GetComponent<HealthPoints>().TakeDamage(damage);
        }
        Destroy(this);
    }
}
