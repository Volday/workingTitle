using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	void Update () {
        if (gameObject.GetComponent<HealthPoints>().currentHealth <= 0) {
            Destroy(gameObject);
        }
	}
}
