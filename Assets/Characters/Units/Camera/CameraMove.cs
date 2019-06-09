using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //параметр передаётся из цели слежения
    public GameObject target;

    public Vector3 cameraOffset;
    public bool smoothing = true;
    public float smoothSpeed;

    private void Start()
    {
        transform.position = target.transform.position + cameraOffset;
        transform.LookAt(target.transform);
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.transform.position + cameraOffset;
        //Сглаживание движения
        if (smoothing)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else {
            transform.position = targetPosition;
        }
    }
}
