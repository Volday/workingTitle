using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePointToBeCarefulDone : MonoBehaviour
{
    TimeManager timeManager;
    public float updateTime = 0;//destroyTime

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(SelfDestroy, updateTime, this);
    }

    void SelfDestroy() {
        Destroy(this);
    }
}
