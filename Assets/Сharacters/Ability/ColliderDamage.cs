using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("sdsd");
        if (collision.gameObject.GetComponent<HealthPoints>() == null)
        {

        }
        Destroy(this);
    }
}
