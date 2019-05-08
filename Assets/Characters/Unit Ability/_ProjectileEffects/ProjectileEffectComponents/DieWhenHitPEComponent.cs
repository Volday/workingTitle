using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieWhenHitPEComponent : MonoBehaviour
{
    Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void Update()
    {
        if (projectile.haveHit) {
            if (projectile.hit.collider.isTrigger == false) {
                if (projectile.hit.collider.gameObject != projectile.owner) {
                    GetComponent<Death>().Die();
                }
            }
        }
    }
}


