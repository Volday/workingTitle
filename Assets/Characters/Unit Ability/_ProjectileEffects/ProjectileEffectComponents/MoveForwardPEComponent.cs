using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardPEComponent : MonoBehaviour
{
    public float speed;
    MoveSpeed moveSpeed;

    void Start()
    {
        moveSpeed = GetComponent<MoveSpeed>();
        moveSpeed.moveSpeed += speed;
    }

    void Update()
    {
        float moveDistance = GetComponent<MoveSpeed>().moveSpeed * Time.deltaTime;
        gameObject.transform.Translate(Vector3.forward * moveDistance);
    }
}
