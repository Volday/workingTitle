using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToDestroy = 0.5f;

    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        TimeManager timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(selfDestroyAfterTime, timeToDestroy, this);
    }

    void selfDestroyAfterTime() {
        Destroy(gameObject);
    }
}
