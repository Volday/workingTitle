using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnValidate()
    {
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
