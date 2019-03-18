using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    UnitManager unitManager;
    HealthPoints healthPoints;
    UnitTeam unitTeam;

    public DestroyGameObjectDeathEffect destroyGameObjectDeathEffect;

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
        int destroyIndex = -1;
        for (int t = 0; t < deathEffects.Length; t++) {
            if (deathEffects[t].name == "DestroyGameObjectDeathEffect")
            {
                destroyIndex = t;
            }
            else {
                deathEffects[t].DoDeathEffect();
            }
        }

        if (destroyIndex > -1) {
            deathEffects[destroyIndex].DoDeathEffect();
        }
    }
}
