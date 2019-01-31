using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
    }
}
