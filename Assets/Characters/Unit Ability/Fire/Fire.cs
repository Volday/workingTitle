using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : UnitAbility
{

    Muzzle muzzle;
    public GameObject bullet;
    public float fireRate = 0.2f;
    float nextShoot = 0;

    private void Start()
    {
        muzzle = GetComponent<Muzzle>();
    }

    void Update()
    {
        if (nextShoot > 0) {
            nextShoot -= Time.deltaTime;
        }
    }

    public override void UseAbility(GameObject abilityTarget)
    {
        if (nextShoot <= 0)
        {
            Instantiate(bullet, muzzle.muzzle.position, transform.rotation);
            nextShoot = fireRate;
        }
    }
}
