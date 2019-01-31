using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        if (target != null) {
            transform.position = new Vector3(target.position.x, target.position.y + 40, target.position.z - 18);
        }
    }
}
