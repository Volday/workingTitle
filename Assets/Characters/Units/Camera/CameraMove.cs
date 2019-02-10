using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float smoothSpeed;
    float smoothDistanceCoefficient = 0;
    public float distanceCoefficient = 0.1f;
    public float minDistance = 1.1f;
    public float minDistanceCoefficient = 1.5f;
    float lastStap = 0;

    private void Start()
    {
        transform.position = target.transform.position + cameraOffset;
        transform.LookAt(target);
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.transform.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        float distanceToMove = Mathf.Sqrt(MyMath.sqrDistanceFromPointToPoint(smoothPosition, transform.position));
        if (distanceToMove > lastStap * minDistanceCoefficient)
        {
            if (lastStap * minDistanceCoefficient > minDistance)
            {
                smoothDistanceCoefficient = lastStap * minDistanceCoefficient;
            }
            else
            {
                smoothDistanceCoefficient = minDistance;
            }
            smoothPosition = Vector3.Lerp(transform.position, smoothPosition, smoothDistanceCoefficient / distanceToMove);
        }
        lastStap = Mathf.Sqrt(MyMath.sqrDistanceFromPointToPoint(smoothPosition, transform.position));
        smoothPosition = Vector3.Lerp(transform.position, smoothPosition, distanceCoefficient);
        transform.position = smoothPosition;
    }
}
