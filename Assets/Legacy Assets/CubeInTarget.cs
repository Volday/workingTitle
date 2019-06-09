using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInTarget : MonoBehaviour
{
    LastStaps lastStaps;
    public GameObject target;
    public float time = 1;
    public float timeBetweenUpdate = 0.001f;
    TimeManager timeManager;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        if (target != null && target.GetComponent<LastStaps>() != null)
        {
            lastStaps = target.GetComponent<LastStaps>();
        }
        timeManager.AddAction(UpdatePosition, timeBetweenUpdate, this);
    }

    void UpdatePosition() {
        if (target != null && target.GetComponent<LastStaps>() != null)
        {
            transform.position = lastStaps.GetMotionVector(time);
        }
        timeManager.AddAction(UpdatePosition, timeBetweenUpdate, this);
    }
}