using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideInTimeComponent : MonoBehaviour
{
    public bool readyDecide = false;

    public void StartDecide(float time) {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        TimeManager timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(SetReadyDecide, time, this);
    }

    void SetReadyDecide()
    {
        readyDecide = true;
    }
}
