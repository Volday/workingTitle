using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikePEComponent : MonoBehaviour
{
    public GameObject vfxProjectile;
    List<GameObject> targets;
    EnemiesAround enemiesAround;
    float radius;
    TimeManager timeManager;

    private void Start()
    {
        radius = GetComponent<Projectile>().radius;
        enemiesAround = GetComponent<EnemiesAround>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(Strike, GetComponent<Projectile>().lifeTime - 0.5f, this);
    }

    private void Update()
    {
        targets = enemiesAround.FindEnemiesAround(radius);
    }

    void Strike() {
        float damage = GetComponent<Projectile>().damage;
        if (targets.Count > 0)
        {
            for (int t = 0; t < targets.Count; t++)
            {
                GameObject newProjectile = Instantiate(vfxProjectile, targets[t].transform.position, Quaternion.identity);
                targets[t].GetComponent<HealthPoints>().TakeDamage(damage / targets.Count);
                ThanderStrikeVFXProjectile thanderStrikeVFXProjectile = newProjectile.GetComponent<ThanderStrikeVFXProjectile>();
                thanderStrikeVFXProjectile.target = targets[t];
                thanderStrikeVFXProjectile.showTime = 0.2f + 0.3f / targets.Count;
            }
        }
        else {
            GameObject newProjectile = Instantiate(vfxProjectile, transform.position, Quaternion.identity);
        }
    }
}
