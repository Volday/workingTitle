using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDemo : MonoBehaviour
{

    public GameObject demo;

    void Update()
    {
        if (Input.GetAxis("1") > 0)
        {
            demo.SetActive(true);
        }
    }
}
