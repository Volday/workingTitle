using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Ability/Fire")]
public class Fire : MonoBehaviour {

    Transform muzzle;
    GameObject bullet;
    float fireRate = 0.2f;
    float cooldown = 0;

    public void Update()
    {

        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if (Input.GetAxis("Fire1") > 0 && cooldown <= 0) {
            Instantiate(bullet, muzzle.position, transform.rotation);
            cooldown = fireRate;
        }
    }
}
