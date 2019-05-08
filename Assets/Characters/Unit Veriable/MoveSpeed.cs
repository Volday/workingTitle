using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public float baseMoveSpeed;
    public float moveSpeed;

    private void Start()
    {
        moveSpeed = baseMoveSpeed;
    }
}
