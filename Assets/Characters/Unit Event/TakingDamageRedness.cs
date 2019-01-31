using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamageRedness : MonoBehaviour
{
    float remainingHealthPoint = 0;
    float swapColor = 0;

    HealthPoints healthPoints;
    Renderer renderer;


    void Start()
    {
        healthPoints = gameObject.GetComponent<HealthPoints>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float currentHealthPoints = healthPoints.currentHealthPoints;

        if (remainingHealthPoint == 0)
        {
            remainingHealthPoint = currentHealthPoints;
        }

        if (swapColor > 0.15f)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            swapColor += Time.deltaTime;
        }

        if (remainingHealthPoint > currentHealthPoints)
        {
            renderer.material.color = Color.red;
            swapColor = 0;
        }

        remainingHealthPoint = currentHealthPoints;
    }
}
