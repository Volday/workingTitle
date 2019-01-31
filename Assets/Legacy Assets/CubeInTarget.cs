using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInTarget : MonoBehaviour
{
    LastStaps lastStaps;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        lastStaps = player.GetComponent<LastStaps>();
    }

    void Update()
    {
        transform.position = lastStaps.GetMotionVector(1f);
    }
}
