using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public float maxHealthPoints;
    public float currentHealthPoints;

    public void TakeDamage(float damage) {
        currentHealthPoints -= damage;
    }

    public void TakeHeal(float heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints) {
            currentHealthPoints = maxHealthPoints;
        }
    }
}
