using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Transform muzzle;
    public GameObject bullet;
    public float fireRate = 0.2f;
    float cooldown = 0;

    private void Update()
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
