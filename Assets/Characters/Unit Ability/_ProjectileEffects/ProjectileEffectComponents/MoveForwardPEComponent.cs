using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardPEComponent : MonoBehaviour
{
    void Update()
    {
        float moveDistance = GetComponent<MoveSpeed>().moveSpeed * Time.deltaTime;
        gameObject.transform.Translate(Vector3.forward * moveDistance);
    }
}
