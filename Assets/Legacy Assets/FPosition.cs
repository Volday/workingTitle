using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPosition : MonoBehaviour
{
    public GameObject gameObject;
    void Update()
    {
        transform.position = gameObject.GetComponent<StateController>().futureTargetPosition;
    }
}
