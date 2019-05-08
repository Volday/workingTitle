using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownPEComponent : MonoBehaviour
{
    float startSpeed;
    MoveSpeed moveSpeedComponent;
    float lifeTime;
    TimeManager timeManager;
    float startTime;

    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        startTime = timeManager.gameTime;

        lifeTime = GetComponent<Projectile>().lifeTime;
        moveSpeedComponent = GetComponent<MoveSpeed>();
        startSpeed = moveSpeedComponent.moveSpeed;
    }

    void Update()
    {
        float timePassed = timeManager.gameTime - startTime;
        if (timePassed <= lifeTime)
        {
            moveSpeedComponent.moveSpeed = startSpeed * (lifeTime - ((timeManager.gameTime - startTime) / lifeTime));
        }
        else {
            Destroy(this);
        }
    }
}
