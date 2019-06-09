using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSizeScale : MonoBehaviour
{
    public AnimationCurve curveOfSize;
    private float resized;
    private TimeManager timeManager;
    private float startTime;
    private Projectile projectile;

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        startTime = timeManager.gameTime;
        projectile = GetComponent<Projectile>();
    }

    void Update()
    {
        resized = curveOfSize.Evaluate((timeManager.gameTime - startTime) / projectile.lifeTime);
        transform.localScale = new Vector3(resized, resized, resized);
    }
}
