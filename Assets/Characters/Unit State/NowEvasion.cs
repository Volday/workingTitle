using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowEvasion : MonoBehaviour
{
    public void StartEvasion(float time)
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        TimeManager timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(EndEvasion, time, this);
    }

    void EndEvasion() {
        Destroy(this);
    }
}
