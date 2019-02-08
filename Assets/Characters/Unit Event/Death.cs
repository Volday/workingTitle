using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    UnitManager unitManager;
    HealthPoints healthPoints;
    UnitTeam unitTeam;

    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
        healthPoints = gameObject.GetComponent<HealthPoints>();
        unitTeam = gameObject.GetComponent<UnitTeam>();
    }

    void Update () {
        float currentHealthPoints = gameObject.GetComponent<HealthPoints>().currentHealthPoints;
        if (currentHealthPoints <= 0 && gameObject.activeSelf == true) {
            Die();
        }
    }

    public void Die() {
        DeathEffect[] deathEffects = GetComponents<DeathEffect>();
        for (int t = 0; t < deathEffects.Length; t++) {
            deathEffects[t].DoDeathEffect();
        }
    }
}
