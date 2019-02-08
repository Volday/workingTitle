using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        UnitManager unitManager = gameManager.GetComponent<UnitManager>();
        int indexProjectile = unitManager.projectiles.IndexOf(gameObject);
        if (indexProjectile != -1) {
            unitManager.projectiles.Remove(gameObject);
        }
    }
}
