using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanderStrikeVFXProjectile : MonoBehaviour
{
    public float showTime = 0.5f;
    public GameObject target;
    private Transform projectileVFX;
    public float shapeRadius = 1;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        TimeManager timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(StopThanderBolt, showTime, this);
        if (target != null)
        {
            projectileVFX = transform.Find("ThunderStrike_PS Variant");
            projectileVFX.gameObject.SetActive(true);
        }
        else {
            projectileVFX = transform.Find("ThunderStrike_PS VariantMistake");
            projectileVFX.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        }
    }

    void StopThanderBolt() {
        projectileVFX.parent = null;
        projectileVFX.GetComponent<ParticleSystem>().Stop(true);
        projectileVFX.gameObject.AddComponent<DestroyAfterTime>();
        Destroy(gameObject);
    }
}
